using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using RPG.Saving;
using RPG.Stats;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float currentHealth;
        [SerializeField] float maxHealth = 100f;
        [SerializeField] [Range(0, 30f)] float despawnTime = 10f; 

        Animator animator;
        BaseStats baseStats;
        GameObject attacker;

        bool isAlive = true;

        public float CurrentHealth { get { return currentHealth; } }
        public float MaxHealth { get { return maxHealth; } }
        public bool IsAlive { get { return isAlive; } }

        public event Action onUnitDeath;
        public event Action<float> onHealthChange;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            baseStats = GetComponent<BaseStats>();
            
            onHealthChange += HandleHeathUpdate;
            baseStats.onLevelChange += HandleLevelChange;

            maxHealth = baseStats.GetStat(Stat.Health);
            currentHealth = maxHealth;

        }

        void OnDestroy()
        {
            onHealthChange -= HandleHeathUpdate;
        }

        public void TakeDamage(float amount, GameObject attacker)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);
            onHealthChange?.Invoke(currentHealth);

            this.attacker = attacker;

        }

        public bool AddHealth(float amount)
        {
            if (currentHealth < maxHealth)
            {
                float healAmount = Mathf.Min(maxHealth - currentHealth, amount);
                currentHealth += healAmount;
                onHealthChange?.Invoke(currentHealth);

                return true;
            }

            return false;
        }

        public object CaptureState()
        {
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            float restoredHealth = (float)state;

            currentHealth = restoredHealth;

            onHealthChange?.Invoke(currentHealth);
        }

        void HandleHeathUpdate(float newHealth)
        {
            if (newHealth == 0)
            {
                Die();
            }
        }

        void HandleLevelChange(int newLevel)
        {
            maxHealth = baseStats.GetStat(Stat.Health);
            currentHealth = maxHealth;

            onHealthChange?.Invoke(currentHealth);
        }

        void Die()
        {
            if (isAlive)
            {
                isAlive = false;
                animator.SetTrigger("die");
                onUnitDeath?.Invoke();

                if (gameObject.tag != "Player")
                {
                    Invoke("Despawn", despawnTime);
                    AwardExperience();
                }
                
            } 
        }

        void Despawn()
        {
            Destroy(gameObject);
        }

        void AwardExperience()
        {
            if (baseStats == null || attacker == null) return;

            float experienceReward = baseStats.GetStat(Stat.ExperienceReward);

            if (attacker.TryGetComponent(out Experience attackerExperience))
            {
                attackerExperience.AddExperience(experienceReward);
            }

            

        }

        
    }
}

