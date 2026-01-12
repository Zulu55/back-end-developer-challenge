import React from 'react';
import { Character } from '../types/Character';
import styles from './CharacterSheet.module.css';

interface CharacterSheetProps {
  character: Character;
}

/**
 * Calculate ability score modifier
 */
function calculateModifier(score: number): number {
  return Math.floor((score - 10) / 2);
}

/**
 * Format modifier with + or - sign
 */
function formatModifier(modifier: number): string {
  return modifier >= 0 ? `+${modifier}` : `${modifier}`;
}

/**
 * Component to display character information
 */
export const CharacterSheet: React.FC<CharacterSheetProps> = ({ character }) => {
  const classNames = character.classes.map(c => c.name).join(', ') || 'No Class';
  
  // Calculate max HP based on level and classes
  const maxHp = character.classes.reduce((total, charClass) => {
    return total + (charClass.hitDiceValue * charClass.classLevel);
  }, 0) || character.level * 10; // fallback calculation

  return (
    <section 
      className={styles.characterSheet}
      aria-label="Character sheet"
    >
      <header className={styles.header}>
        <h1 
          className={styles.characterName}
          id="character-name"
        >
          {character.name}
        </h1>
        <p className={styles.classLevel}>
          <span aria-label="Character class">{classNames}</span>
          {' • '}
          <span aria-label="Character level">Level {character.level}</span>
        </p>
      </header>

      <div className={styles.hitPointsSection}>
        <div className={styles.hitPointsGrid}>
          <div className={styles.hpBox}>
            <span className={styles.hpLabel} id="current-hp-label">
              Current HP
            </span>
            <div 
              className={`${styles.hpValue} ${styles.currentHp}`}
              aria-labelledby="current-hp-label"
              role="status"
              aria-live="polite"
            >
              {character.hitPoints}
            </div>
          </div>

          <div className={styles.hpBox}>
            <span className={styles.hpLabel} id="max-hp-label">
              Maximum HP
            </span>
            <div 
              className={`${styles.hpValue} ${styles.maxHp}`}
              aria-labelledby="max-hp-label"
            >
              {maxHp}
            </div>
          </div>

          <div className={styles.hpBox}>
            <span className={styles.hpLabel} id="temp-hp-label">
              Temporary HP
            </span>
            <div 
              className={`${styles.hpValue} ${styles.tempHp}`}
              aria-labelledby="temp-hp-label"
              role="status"
              aria-live="polite"
            >
              {character.tempHitPoints}
            </div>
          </div>
        </div>
      </div>

      {character.stats && (
        <div className={styles.statsSection}>
          <h2 className={styles.sectionTitle}>Ability Scores</h2>
          <div className={styles.statsGrid}>
            <div className={styles.statBox}>
              <div className={styles.statName} id="str-label">Strength</div>
              <div 
                className={styles.statValue}
                aria-labelledby="str-label"
              >
                {character.stats.strength}
              </div>
              <div 
                className={styles.statModifier}
                aria-label={`Strength modifier ${formatModifier(calculateModifier(character.stats.strength))}`}
              >
                {formatModifier(calculateModifier(character.stats.strength))}
              </div>
            </div>

            <div className={styles.statBox}>
              <div className={styles.statName} id="dex-label">Dexterity</div>
              <div 
                className={styles.statValue}
                aria-labelledby="dex-label"
              >
                {character.stats.dexterity}
              </div>
              <div 
                className={styles.statModifier}
                aria-label={`Dexterity modifier ${formatModifier(calculateModifier(character.stats.dexterity))}`}
              >
                {formatModifier(calculateModifier(character.stats.dexterity))}
              </div>
            </div>

            <div className={styles.statBox}>
              <div className={styles.statName} id="con-label">Constitution</div>
              <div 
                className={styles.statValue}
                aria-labelledby="con-label"
              >
                {character.stats.constitution}
              </div>
              <div 
                className={styles.statModifier}
                aria-label={`Constitution modifier ${formatModifier(calculateModifier(character.stats.constitution))}`}
              >
                {formatModifier(calculateModifier(character.stats.constitution))}
              </div>
            </div>

            <div className={styles.statBox}>
              <div className={styles.statName} id="int-label">Intelligence</div>
              <div 
                className={styles.statValue}
                aria-labelledby="int-label"
              >
                {character.stats.intelligence}
              </div>
              <div 
                className={styles.statModifier}
                aria-label={`Intelligence modifier ${formatModifier(calculateModifier(character.stats.intelligence))}`}
              >
                {formatModifier(calculateModifier(character.stats.intelligence))}
              </div>
            </div>

            <div className={styles.statBox}>
              <div className={styles.statName} id="wis-label">Wisdom</div>
              <div 
                className={styles.statValue}
                aria-labelledby="wis-label"
              >
                {character.stats.wisdom}
              </div>
              <div 
                className={styles.statModifier}
                aria-label={`Wisdom modifier ${formatModifier(calculateModifier(character.stats.wisdom))}`}
              >
                {formatModifier(calculateModifier(character.stats.wisdom))}
              </div>
            </div>

            <div className={styles.statBox}>
              <div className={styles.statName} id="cha-label">Charisma</div>
              <div 
                className={styles.statValue}
                aria-labelledby="cha-label"
              >
                {character.stats.charisma}
              </div>
              <div 
                className={styles.statModifier}
                aria-label={`Charisma modifier ${formatModifier(calculateModifier(character.stats.charisma))}`}
              >
                {formatModifier(calculateModifier(character.stats.charisma))}
              </div>
            </div>
          </div>
        </div>
      )}
    </section>
  );
};
