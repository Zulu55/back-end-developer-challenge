namespace DnDBeyond.Shared.Entities;

/// <summary>
/// Item/Object
/// </summary>
public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Foreign key
    public int CharacterId { get; set; }

    public Character Character { get; set; } = null!;

    // Navigation property
    public ItemModifier? Modifier { get; set; }
}