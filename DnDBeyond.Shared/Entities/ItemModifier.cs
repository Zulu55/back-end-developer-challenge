using DnDBeyond.Shared.Enums;

namespace DnDBeyond.Shared.Entities;

/// <summary>
/// Item modifier (1:1 relationship with Item)
/// </summary>
public class ItemModifier
{
    public int Id { get; set; }
    public string AffectedObject { get; set; } = string.Empty;
    public StatType AffectedValue { get; set; }
    public int Value { get; set; }

    // Foreign key
    public int ItemId { get; set; }

    public Item Item { get; set; } = null!;
}