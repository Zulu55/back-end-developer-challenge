using DnDBeyond.Backend.Controllers;
using DnDBeyond.Backend.Data;
using DnDBeyond.Shared.Entities;
using DnDBeyond.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnDBeyond.Backend.Tests;

[TestClass]
public class CharactersControllerTests
{
    private GameDbContext CreateInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<GameDbContext>()
        .UseInMemoryDatabase(dbName)
        .Options;
        return new GameDbContext(options);
    }

    [TestMethod]
    public async Task DealDamage_RespectsImmunityAndTempHp()
    {
        using var context = CreateInMemoryContext("DealDamage_RespectsImmunityAndTempHp");
        // Arrange
        var character = new Character { Name = "Eldric", HitPoints = 11, TempHitPoints = 10 };
        character.Defenses.Add(new Defense { Type = DamageType.Piercing, DefenseType = DefenseType.Immunity });
        context.Characters.Add(character);
        await context.SaveChangesAsync();

        var controller = new CharactersController(context);
        var req = new CharactersController.DamageRequest(14, DamageType.Piercing);

        // Act
        var result = await controller.DealDamage(character.Id, req);

        // Assert
        var ok = result.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        var updated = ok.Value as Character;
        Assert.IsNotNull(updated);
        // Immunity -> no damage, temp and HP unchanged
        Assert.AreEqual(10, updated.TempHitPoints);
        Assert.AreEqual(11, updated.HitPoints);
    }

    [TestMethod]
    public async Task DealDamage_AppliesResistance_Vulnerability_TempHpOrder()
    {
        using var context = CreateInMemoryContext("DealDamage_AppliesResistance_Vulnerability_TempHpOrder");
        // Arrange
        var character = new Character { Name = "Test", HitPoints = 20, TempHitPoints = 5 };
        character.Defenses.Add(new Defense { Type = DamageType.Piercing, DefenseType = DefenseType.Resistance });
        context.Characters.Add(character);
        await context.SaveChangesAsync();

        var controller = new CharactersController(context);
        // Damage11 -> resistance halves to5 (int division), temp5 consumed, remaining0
        var req = new CharactersController.DamageRequest(11, DamageType.Piercing);
        var result = await controller.DealDamage(character.Id, req);
        var ok = result.Result as OkObjectResult;
        var updated = ok.Value as Character;
        Assert.AreEqual(0, updated.TempHitPoints);
        Assert.AreEqual(20, updated.HitPoints);
    }

    [TestMethod]
    public async Task Heal_IncreasesHitPoints()
    {
        using var context = CreateInMemoryContext("Heal_IncreasesHitPoints");
        var character = new Character { Name = "Healee", HitPoints = 5 };
        context.Characters.Add(character);
        await context.SaveChangesAsync();

        var controller = new CharactersController(context);
        var req = new CharactersController.HealRequest(10);
        var result = await controller.Heal(character.Id, req);
        var ok = result.Result as OkObjectResult;
        var updated = ok.Value as Character;
        Assert.AreEqual(15, updated.HitPoints);
    }

    [TestMethod]
    public async Task AddTempHp_TakesHigherValue()
    {
        using var context = CreateInMemoryContext("AddTempHp_TakesHigherValue");
        var character = new Character { Name = "Tempster", HitPoints = 30, TempHitPoints = 3 };
        context.Characters.Add(character);
        await context.SaveChangesAsync();

        var controller = new CharactersController(context);
        var req = new CharactersController.TempHpRequest(10);
        var result = await controller.AddTempHp(character.Id, req);
        var ok = result.Result as OkObjectResult;
        var updated = ok.Value as Character;
        Assert.AreEqual(10, updated.TempHitPoints);
    }

    [TestMethod]
    public async Task GetAll_ReturnsAllCharacters()
    {
        using var context = CreateInMemoryContext("GetAll_ReturnsAllCharacters");
        // Arrange
        var c1 = new Character { Name = "A", HitPoints = 10 };
        var c2 = new Character { Name = "B", HitPoints = 20 };
        context.Characters.AddRange(c1, c2);
        await context.SaveChangesAsync();

        var controller = new CharactersController(context);

        // Act
        var result = await controller.GetAll();

        // Assert
        var ok = result.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        var list = ok.Value as IEnumerable<Character>;
        Assert.IsNotNull(list);
        Assert.AreEqual(2, list.Count());
        CollectionAssert.AreEquivalent(new[] { "A", "B" }, list.Select(ch => ch.Name).ToArray());
    }

    [TestMethod]
    public async Task GetById_ReturnsCharacter_WhenExists()
    {
        using var context = CreateInMemoryContext("GetById_ReturnsCharacter_WhenExists");
        // Arrange
        var c = new Character { Name = "Solo", HitPoints = 7 };
        context.Characters.Add(c);
        await context.SaveChangesAsync();

        var controller = new CharactersController(context);

        // Act
        var result = await controller.GetById(c.Id);

        // Assert
        var ok = result.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        var returned = ok.Value as Character;
        Assert.IsNotNull(returned);
        Assert.AreEqual("Solo", returned.Name);
        Assert.AreEqual(7, returned.HitPoints);
    }
}