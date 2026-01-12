using DnDBeyond.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDBeyond.Backend.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
    }

    public DbSet<Character> Characters => Set<Character>();
    public DbSet<CharacterClass> CharacterClasses => Set<CharacterClass>();
    public DbSet<Stats> Stats => Set<Stats>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemModifier> ItemModifiers => Set<ItemModifier>();
    public DbSet<Defense> Defenses => Set<Defense>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1:1 relationship Character -> Stats
        modelBuilder.Entity<Character>()
            .HasOne(c => c.Stats)
            .WithOne(s => s.Character)
            .HasForeignKey<Stats>(s => s.CharacterId);

        // 1:1 relationship Item -> ItemModifier
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Modifier)
            .WithOne(m => m.Item)
            .HasForeignKey<ItemModifier>(m => m.ItemId);

        // Store enums as strings in DB (more readable)
        modelBuilder.Entity<Defense>()
            .Property(d => d.Type)
            .HasMaxLength(20);

        modelBuilder.Entity<Defense>()
            .Property(d => d.DefenseType)
            .HasMaxLength(20);

        modelBuilder.Entity<ItemModifier>()
            .Property(m => m.AffectedValue)
            .HasMaxLength(20);
    }
}