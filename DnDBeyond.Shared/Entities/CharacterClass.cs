namespace DnDBeyond.Shared.Entities;

/// <summary>
/// Character class (fighter, wizard, etc.)
/// </summary>
public class CharacterClass
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int HitDiceValue { get; set; }
    public int ClassLevel { get; set; }

    // Foreign key
    public int CharacterId { get; set; }

    public Character Character { get; set; } = null!;
}