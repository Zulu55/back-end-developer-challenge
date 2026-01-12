import React, { useState } from 'react';
import { DamageType } from '../types/Character';
import { dealDamage, healCharacter, setTempHitPoints } from '../services/characterService';
import styles from './ActionControls.module.css';

interface ActionControlsProps {
  characterId: number;
  onActionComplete: () => void;
}

/**
 * Component for character actions: damage, heal, and temp HP
 */
export const ActionControls: React.FC<ActionControlsProps> = ({ characterId, onActionComplete }) => {
  const [damageAmount, setDamageAmount] = useState<string>('');
  const [damageType, setDamageType] = useState<DamageType>(DamageType.Slashing);
  const [healAmount, setHealAmount] = useState<string>('');
  const [tempHpAmount, setTempHpAmount] = useState<string>('');
  
  const [loading, setLoading] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const damageTypes = Object.values(DamageType);

  /**
   * Handle deal damage action
   */
  const handleDealDamage = async (e: React.FormEvent) => {
    e.preventDefault();
    
    const amount = parseInt(damageAmount, 10);
    if (isNaN(amount) || amount <= 0) {
      setError('Damage amount must be a positive number');
      return;
    }

    setLoading('damage');
    setError(null);
    setSuccess(null);

    try {
      await dealDamage(characterId, { amount, type: damageType });
      setSuccess(`Dealt ${amount} ${damageType} damage`);
      setDamageAmount('');
      onActionComplete();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to deal damage');
    } finally {
      setLoading(null);
    }
  };

  /**
   * Handle heal action
   */
  const handleHeal = async (e: React.FormEvent) => {
    e.preventDefault();
    
    const amount = parseInt(healAmount, 10);
    if (isNaN(amount) || amount <= 0) {
      setError('Heal amount must be a positive number');
      return;
    }

    setLoading('heal');
    setError(null);
    setSuccess(null);

    try {
      await healCharacter(characterId, { amount });
      setSuccess(`Healed ${amount} HP`);
      setHealAmount('');
      onActionComplete();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to heal character');
    } finally {
      setLoading(null);
    }
  };

  /**
   * Handle set temporary HP action
   */
  const handleSetTempHp = async (e: React.FormEvent) => {
    e.preventDefault();
    
    const amount = parseInt(tempHpAmount, 10);
    if (isNaN(amount) || amount < 0) {
      setError('Temporary HP must be a non-negative number');
      return;
    }

    setLoading('tempHp');
    setError(null);
    setSuccess(null);

    try {
      await setTempHitPoints(characterId, { amount });
      setSuccess(`Set temporary HP to ${amount}`);
      setTempHpAmount('');
      onActionComplete();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to set temporary HP');
    } finally {
      setLoading(null);
    }
  };

  return (
    <section 
      className={styles.actionControls}
      aria-label="Character actions"
    >
      <h2 className={styles.title}>Actions</h2>

      <div className={styles.actionsGrid}>
        {/* Deal Damage Form */}
        <form 
          className={styles.actionForm}
          onSubmit={handleDealDamage}
          aria-labelledby="damage-form-title"
        >
          <h3 
            className={styles.formTitle}
            id="damage-form-title"
          >
            Deal Damage
          </h3>
          
          <div className={styles.formGroup}>
            <label htmlFor="damage-amount" className={styles.label}>
              Amount
            </label>
            <input
              id="damage-amount"
              type="number"
              className={styles.input}
              value={damageAmount}
              onChange={(e) => setDamageAmount(e.target.value)}
              min="1"
              placeholder="Enter damage amount"
              aria-required="true"
              disabled={loading === 'damage'}
            />
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="damage-type" className={styles.label}>
              Damage Type
            </label>
            <select
              id="damage-type"
              className={styles.select}
              value={damageType}
              onChange={(e) => setDamageType(e.target.value as DamageType)}
              aria-required="true"
              disabled={loading === 'damage'}
            >
              {damageTypes.map((type) => (
                <option key={type} value={type}>
                  {type}
                </option>
              ))}
            </select>
          </div>

          <button
            type="submit"
            className={`${styles.button} ${styles.damageButton}`}
            disabled={loading === 'damage'}
            aria-label="Deal damage to character"
          >
            {loading === 'damage' ? 'Dealing Damage...' : 'Deal Damage'}
          </button>
        </form>

        {/* Heal Form */}
        <form 
          className={styles.actionForm}
          onSubmit={handleHeal}
          aria-labelledby="heal-form-title"
        >
          <h3 
            className={styles.formTitle}
            id="heal-form-title"
          >
            Heal
          </h3>
          
          <div className={styles.formGroup}>
            <label htmlFor="heal-amount" className={styles.label}>
              Amount
            </label>
            <input
              id="heal-amount"
              type="number"
              className={styles.input}
              value={healAmount}
              onChange={(e) => setHealAmount(e.target.value)}
              min="1"
              placeholder="Enter heal amount"
              aria-required="true"
              disabled={loading === 'heal'}
            />
          </div>

          <button
            type="submit"
            className={`${styles.button} ${styles.healButton}`}
            disabled={loading === 'heal'}
            aria-label="Heal character"
          >
            {loading === 'heal' ? 'Healing...' : 'Heal'}
          </button>
        </form>

        {/* Set Temporary HP Form */}
        <form 
          className={styles.actionForm}
          onSubmit={handleSetTempHp}
          aria-labelledby="temp-hp-form-title"
        >
          <h3 
            className={styles.formTitle}
            id="temp-hp-form-title"
          >
            Set Temporary HP
          </h3>
          
          <div className={styles.formGroup}>
            <label htmlFor="temp-hp-amount" className={styles.label}>
              Amount
            </label>
            <input
              id="temp-hp-amount"
              type="number"
              className={styles.input}
              value={tempHpAmount}
              onChange={(e) => setTempHpAmount(e.target.value)}
              min="0"
              placeholder="Enter temp HP amount"
              aria-required="true"
              disabled={loading === 'tempHp'}
            />
          </div>

          <button
            type="submit"
            className={`${styles.button} ${styles.tempHpButton}`}
            disabled={loading === 'tempHp'}
            aria-label="Set temporary hit points"
          >
            {loading === 'tempHp' ? 'Setting Temp HP...' : 'Set Temporary HP'}
          </button>
        </form>
      </div>

      {/* Error Message */}
      {error && (
        <div 
          className={styles.error}
          role="alert"
          aria-live="assertive"
        >
          {error}
        </div>
      )}

      {/* Success Message */}
      {success && (
        <div 
          className={styles.success}
          role="status"
          aria-live="polite"
        >
          {success}
        </div>
      )}
    </section>
  );
};
