using DnDBeyond.Shared.Enums;

namespace DnDBeyond.Shared.Entities;

/// <summary>
/// Defenses (immunity, resistance, vulnerability)
/// </summary>
public class Defense
{
    public int Id { get; set; }
    public DamageType Type { get; set; }
    public DefenseType DefenseType { get; set; }

    // Foreign key
    public int CharacterId { get; set; }

    public Character Character { get; set; } = null!;
}