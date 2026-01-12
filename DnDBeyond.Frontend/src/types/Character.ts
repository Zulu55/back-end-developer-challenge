export enum DamageType {
  Bludgeoning = 'Bludgeoning',
  Piercing = 'Piercing',
  Slashing = 'Slashing',
  Fire = 'Fire',
  Cold = 'Cold',
  Acid = 'Acid',
  Thunder = 'Thunder',
  Lightning = 'Lightning',
  Poison = 'Poison',
  Radiant = 'Radiant',
  Necrotic = 'Necrotic',
  Psychic = 'Psychic',
  Force = 'Force'
}

export enum DefenseType {
  Immunity = 'Immunity',
  Resistance = 'Resistance',
  Vulnerability = 'Vulnerability'
}

export interface Stats {
  id: number;
  strength: number;
  dexterity: number;
  constitution: number;
  intelligence: number;
  wisdom: number;
  charisma: number;
  characterId: number;
}

export interface CharacterClass {
  id: number;
  name: string;
  hitDiceValue: number;
  classLevel: number;
  characterId: number;
}

export interface Defense {
  id: number;
  type: DamageType;
  defenseType: DefenseType;
  characterId: number;
}

export interface ItemModifier {
  id: number;
  affectedObject: string;
  affectedValue: string;
  value: number;
}

export interface Item {
  id: number;
  name: string;
  modifier?: ItemModifier;
  characterId: number;
}

export interface Character {
  id: number;
  name: string;
  level: number;
  hitPoints: number;
  tempHitPoints: number;
  stats?: Stats;
  classes: CharacterClass[];
  items: Item[];
  defenses: Defense[];
}

export interface DamageRequest {
  amount: number;
  type: DamageType;
}

export interface HealRequest {
  amount: number;
}

export interface TempHpRequest {
  amount: number;
}
