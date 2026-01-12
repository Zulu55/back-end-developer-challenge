import { Character, DamageRequest, HealRequest, TempHpRequest } from '../types/Character';

const API_BASE_URL = '/api';

/**
 * Fetch a character by ID
 */
export async function getCharacter(id: number): Promise<Character> {
  const response = await fetch(`${API_BASE_URL}/characters/${id}`);
  
  if (!response.ok) {
    throw new Error(`Failed to fetch character: ${response.statusText}`);
  }
  
  return response.json();
}

/**
 * Fetch all characters
 */
export async function getAllCharacters(): Promise<Character[]> {
  const response = await fetch(`${API_BASE_URL}/characters`);
  
  if (!response.ok) {
    throw new Error(`Failed to fetch characters: ${response.statusText}`);
  }
  
  return response.json();
}

/**
 * Deal damage to a character
 */
export async function dealDamage(id: number, damageRequest: DamageRequest): Promise<Character> {
  const response = await fetch(`${API_BASE_URL}/characters/${id}/damage`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(damageRequest),
  });
  
  if (!response.ok) {
    const error = await response.text();
    throw new Error(error || `Failed to deal damage: ${response.statusText}`);
  }
  
  return response.json();
}

/**
 * Heal a character
 */
export async function healCharacter(id: number, healRequest: HealRequest): Promise<Character> {
  const response = await fetch(`${API_BASE_URL}/characters/${id}/heal`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(healRequest),
  });
  
  if (!response.ok) {
    const error = await response.text();
    throw new Error(error || `Failed to heal character: ${response.statusText}`);
  }
  
  return response.json();
}

/**
 * Set temporary hit points for a character
 */
export async function setTempHitPoints(id: number, tempHpRequest: TempHpRequest): Promise<Character> {
  const response = await fetch(`${API_BASE_URL}/characters/${id}/temp-hp`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(tempHpRequest),
  });
  
  if (!response.ok) {
    const error = await response.text();
    throw new Error(error || `Failed to set temp HP: ${response.statusText}`);
  }
  
  return response.json();
}
