import React, { useState, useEffect, useCallback } from 'react';
import { Character } from './types/Character';
import { getCharacter, getAllCharacters } from './services/characterService';
import { CharacterSheet } from './components/CharacterSheet';
import { ActionControls } from './components/ActionControls';
import styles from './App.module.css';

const REFRESH_INTERVAL = 5000; // 5 seconds

function App() {
  const [characters, setCharacters] = useState<Character[]>([]);
  const [selectedCharacterId, setSelectedCharacterId] = useState<number | null>(null);
  const [character, setCharacter] = useState<Character | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  /**
   * Fetch the list of all characters
   */
  const fetchCharacters = useCallback(async () => {
    try {
      const chars = await getAllCharacters();
      setCharacters(chars);
      
      // Auto-select first character if none selected
      if (chars.length > 0 && selectedCharacterId === null) {
        setSelectedCharacterId(chars[0].id);
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load characters');
    }
  }, [selectedCharacterId]);

  /**
   * Fetch the selected character's data
   */
  const fetchCharacter = useCallback(async () => {
    if (selectedCharacterId === null) return;

    try {
      const char = await getCharacter(selectedCharacterId);
      setCharacter(char);
      setError(null);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load character');
    } finally {
      setLoading(false);
    }
  }, [selectedCharacterId]);

  /**
   * Initial load
   */
  useEffect(() => {
    fetchCharacters();
  }, [fetchCharacters]);

  /**
   * Load character when selection changes
   */
  useEffect(() => {
    if (selectedCharacterId !== null) {
      setLoading(true);
      fetchCharacter();
    }
  }, [selectedCharacterId, fetchCharacter]);

  /**
   * Auto-refresh character data periodically
   */
  useEffect(() => {
    if (selectedCharacterId === null) return;

    const interval = setInterval(() => {
      fetchCharacter();
    }, REFRESH_INTERVAL);

    return () => clearInterval(interval);
  }, [selectedCharacterId, fetchCharacter]);

  /**
   * Handle action completion (refresh character)
   */
  const handleActionComplete = () => {
    fetchCharacter();
  };

  /**
   * Handle character selection change
   */
  const handleCharacterChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const id = parseInt(e.target.value, 10);
    setSelectedCharacterId(id);
  };

  return (
    <div className={styles.app}>
      <header className={styles.header}>
        <h1 className={styles.appTitle}>D&D Beyond Character Sheet</h1>
        <p className={styles.subtitle}>Manage your character's stats and actions</p>
      </header>

      {characters.length > 1 && (
        <div className={styles.characterSelector}>
          <label htmlFor="character-select" className={styles.selectorLabel}>
            Select Character:
          </label>
          <select
            id="character-select"
            className={styles.select}
            value={selectedCharacterId ?? ''}
            onChange={handleCharacterChange}
          >
            {characters.map((char) => (
              <option key={char.id} value={char.id}>
                {char.name} - Level {char.level}
              </option>
            ))}
          </select>
        </div>
      )}

      {loading && (
        <div className={styles.loading} role="status" aria-live="polite">
          Loading character...
        </div>
      )}

      {error && !loading && (
        <div className={styles.error} role="alert">
          <h2 className={styles.errorTitle}>Error</h2>
          <p className={styles.errorMessage}>{error}</p>
        </div>
      )}

      {character && !loading && (
        <>
          <CharacterSheet character={character} />
          <ActionControls 
            characterId={character.id} 
            onActionComplete={handleActionComplete}
          />
          <div className={styles.autoRefreshNote} role="status" aria-live="polite">
            Character data auto-refreshes every {REFRESH_INTERVAL / 1000} seconds
          </div>
        </>
      )}
    </div>
  );
}

export default App;
