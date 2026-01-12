namespace DnDBeyond.Shared.Entities
{
    /// <summary>
    /// Main entity: Character
    /// </summary>
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int HitPoints { get; set; }

        // Temporary hit points (not additive; store the current temporary HP)
        public int TempHitPoints { get; set; }

        // Navigation properties
        public Stats? Stats { get; set; }

        public ICollection<CharacterClass> Classes { get; set; } = new List<CharacterClass>();
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public ICollection<Defense> Defenses { get; set; } = new List<Defense>();
    }
}