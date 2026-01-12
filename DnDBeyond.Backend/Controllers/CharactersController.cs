using DnDBeyond.Backend.Data;
using DnDBeyond.Shared.Entities;
using DnDBeyond.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnDBeyond.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly GameDbContext _context;

    public CharactersController(GameDbContext context)
    {
        _context = context;
    }

    // GET: api/Characters
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetAll()
    {
        var characters = await _context.Characters
            .Include(c => c.Classes)
            .Include(c => c.Items)
            .ThenInclude(i => i.Modifier)
            .Include(c => c.Defenses)
            .Include(c => c.Stats)
            .ToListAsync();

        return Ok(characters);
    }

    // GET: api/Characters/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Character>> GetById(int id)
    {
        var character = await _context.Characters
            .Include(c => c.Classes)
            .Include(c => c.Items)
            .ThenInclude(i => i.Modifier)
            .Include(c => c.Defenses)
            .Include(c => c.Stats)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (character == null) return NotFound();
        return Ok(character);
    }

    // DTOs
    public record DamageRequest(int Amount, DamageType Type);
    public record HealRequest(int Amount);
    public record TempHpRequest(int Amount);

    // POST: api/Characters/{id}/damage
    [HttpPost("{id}/damage")]
    public async Task<ActionResult<Character>> DealDamage(int id, [FromBody] DamageRequest req)
    {
        if (req.Amount <= 0)
        {
            return BadRequest("Damage amount must be positive.");
        }

        var character = await _context.Characters
            .Include(c => c.Defenses)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (character == null)
        {
            return NotFound();
        }

        int damage = req.Amount;

        // Evaluate defenses for this damage type
        if (character.Defenses != null)
        {
            if (character.Defenses.Any(d => d.Type == req.Type && d.DefenseType == DefenseType.Immunity))
            {
                damage = 0;
            }
            else if (character.Defenses.Any(d => d.Type == req.Type && d.DefenseType == DefenseType.Vulnerability))
            {
                damage = checked(damage * 2);
            }
            else if (character.Defenses.Any(d => d.Type == req.Type && d.DefenseType == DefenseType.Resistance))
            {
                damage = damage / 2; // integer division
            }
        }

        // Apply temporary hit points first
        int remaining = damage;
        if (character.TempHitPoints > 0)
        {
            int usedTemp = Math.Min(character.TempHitPoints, remaining);
            character.TempHitPoints -= usedTemp;
            remaining -= usedTemp;
        }

        // Apply remaining damage to HitPoints
        if (remaining > 0)
        {
            character.HitPoints = Math.Max(0, character.HitPoints - remaining);
        }

        await _context.SaveChangesAsync();
        return Ok(character);
    }

    // POST: api/Characters/{id}/heal
    [HttpPost("{id}/heal")]
    public async Task<ActionResult<Character>> Heal(int id, [FromBody] HealRequest req)
    {
        if (req.Amount <= 0)
        {
            return BadRequest("Heal amount must be positive.");
        }

        var character = await _context.Characters.FindAsync(id);
        if (character == null)
        {
            return NotFound();
        }

        character.HitPoints += req.Amount;
        await _context.SaveChangesAsync();

        return Ok(character);
    }

    // POST: api/Characters/{id}/temp-hp
    [HttpPost("{id}/temp-hp")]
    public async Task<ActionResult<Character>> AddTempHp(int id, [FromBody] TempHpRequest req)
    {
        if (req.Amount < 0)
        {
            return BadRequest("Temp HP amount must be non-negative.");
        }

        var character = await _context.Characters.FindAsync(id);
        if (character == null) return NotFound();

        // Temp HP are not additive; set to the higher value
        character.TempHitPoints = System.Math.Max(character.TempHitPoints, req.Amount);
        await _context.SaveChangesAsync();

        return Ok(character);
    }
}