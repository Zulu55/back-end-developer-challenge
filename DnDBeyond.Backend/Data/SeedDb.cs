using DnDBeyond.Shared.Entities;
using DnDBeyond.Shared.Enums;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDBeyond.Backend.Data;

public class SeedDb
{
    private readonly GameDbContext _context;

    public SeedDb(GameDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCharactersAsync();
    }

    private async Task CheckCharactersAsync()
    {
        if (!_context.Characters.Any())
        {
            _context.Characters.AddRange(
                CreateBriv(),
                CreateElara(),
                CreateThorm(),
                CreateNyx(),
                CreateLira()
            );

            await _context.SaveChangesAsync();
        }
    }

    // Individual character factory methods
    private Character CreateBriv()
    {
        return new Character
        {
            Name = "Briv",
            Level = 5,
            HitPoints = 25,
            Classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Name = "Fighter",
                    HitDiceValue = 10,
                    ClassLevel = 5
                },
                new CharacterClass
                {
                    Name = "Rogue",
                    HitDiceValue = 8,
                    ClassLevel = 2
                }
            },
            Defenses = new List<Defense>
            {
                new Defense
                {
                    Type = DamageType.Fire,
                    DefenseType = DefenseType.Resistance
                },
                new Defense
                {
                    Type = DamageType.Force,
                    DefenseType = DefenseType.Immunity
                }
            },
            Items = new List<Item>
            {
                new Item
                {
                    Name = "Longsword +1",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "attack",
                        AffectedValue = StatType.Strength,
                        Value = 1
                    }
                },
                new Item
                {
                    Name = "Ioun Stone of Fortitude",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "stats",
                        AffectedValue = StatType.Constitution,
                        Value = 2
                    }
                }
            },
            Stats = new Stats
            {
                Strength = 16,
                Dexterity = 14,
                Constitution = 15,
                Intelligence = 10,
                Wisdom = 12,
                Charisma = 8
            }
        };
    }

    private Character CreateElara()
    {
        return new Character
        {
            Name = "Elara",
            Level = 7,
            HitPoints = 30,
            Classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Name = "Wizard",
                    HitDiceValue = 6,
                    ClassLevel = 7
                }
            },
            Defenses = new List<Defense>
            {
                // use Force as a proxy for arcane/magical defense
                new Defense
                {
                    Type = DamageType.Force,
                    DefenseType = DefenseType.Resistance
                }
            },
            Items = new List<Item>
            {
                new Item
                {
                    Name = "Wand of the Magi",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "spellcasting",
                        AffectedValue = StatType.Intelligence,
                        Value = 2
                    }
                },
                new Item
                {
                    Name = "Cloak of Protection",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "ac",
                        AffectedValue = StatType.Dexterity,
                        Value = 1
                    }
                }
            },
            Stats = new Stats
            {
                Strength = 8,
                Dexterity = 14,
                Constitution = 12,
                Intelligence = 18,
                Wisdom = 13,
                Charisma = 11
            }
        };
    }

    private Character CreateThorm()
    {
        return new Character
        {
            Name = "Thorm",
            Level = 6,
            HitPoints = 48,
            Classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Name = "Paladin",
                    HitDiceValue = 10,
                    ClassLevel = 6
                }
            },
            Defenses = new List<Defense>
            {
                new Defense
                {
                    Type = DamageType.Radiant,
                    DefenseType = DefenseType.Immunity
                },
                new Defense
                {
                    Type = DamageType.Poison,
                    DefenseType = DefenseType.Resistance
                }
            },
            Items = new List<Item>
            {
                new Item
                {
                    Name = "Holy Avenger",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "attack",
                        AffectedValue = StatType.Strength,
                        Value = 2
                    }
                },
                new Item
                {
                    Name = "Shield of Faith",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "ac",
                        AffectedValue = StatType.Dexterity,
                        Value = 1
                    }
                }
            },
            Stats = new Stats
            {
                Strength = 18,
                Dexterity = 10,
                Constitution = 16,
                Intelligence = 9,
                Wisdom = 13,
                Charisma = 14
            }
        };
    }

    private Character CreateNyx()
    {
        return new Character
        {
            Name = "Nyx",
            Level = 5,
            HitPoints = 28,
            Classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Name = "Rogue",
                    HitDiceValue = 8,
                    ClassLevel = 5
                }
            },
            Defenses = new List<Defense>
            {
                new Defense
                {
                    Type = DamageType.Psychic,
                    DefenseType = DefenseType.Vulnerability
                }
            },
            Items = new List<Item>
            {
                new Item
                {
                    Name = "Dagger of Venom",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "attack",
                        AffectedValue = StatType.Dexterity,
                        Value = 1
                    }
                },
                new Item
                {
                    Name = "Boots of Elvenkind",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "stealth",
                        AffectedValue = StatType.Dexterity,
                        Value = 2
                    }
                }
            },
            Stats = new Stats
            {
                Strength = 10,
                Dexterity = 18,
                Constitution = 12,
                Intelligence = 13,
                Wisdom = 11,
                Charisma = 14
            }
        };
    }

    private Character CreateLira()
    {
        return new Character
        {
            Name = "Lira",
            Level = 4,
            HitPoints = 22,
            Classes = new List<CharacterClass>
            {
                new CharacterClass
                {
                    Name = "Bard",
                    HitDiceValue = 8,
                    ClassLevel = 4
                }
            },
            Defenses = new List<Defense>
            {
                new Defense
                {
                    Type = DamageType.Thunder,
                    DefenseType = DefenseType.Resistance
                }
            },
            Items = new List<Item>
            {
                new Item
                {
                    Name = "Lute of Soothing",
                    Modifier = new ItemModifier
                    {
                        AffectedObject = "performance",
                        AffectedValue = StatType.Charisma,
                        Value = 2
                    }
                }
            },
            Stats = new Stats
            {
                Strength = 9,
                Dexterity = 13,
                Constitution = 12,
                Intelligence = 11,
                Wisdom = 10,
                Charisma = 17
            }
        };
    }
}