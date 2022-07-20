using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

using GameDevTV.Utils;

using RPG.Saving;
using RPG.Stats;


namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] [Range(0, 30f)] float despawnTime = 10f;

        Animator animator;
        BaseStats baseStats;
        GameObject attacker;

        float currentHealth = -1f;
        bool isAlive = true;
        LazyValue<float> maxHealth;
        

        public float CurrentHealth { get { return currentHealth; } }
        public float MaxHealth { get { return maxHealth.value; } }
        public bool IsAlive { get { return isAlive; } }

        public UnityEvent onUnitDeath;
        public HealthChangeEvent onHealthChange;
        public DamageTakenEvent onDamageTaken;

        [Serializable]
        public class HealthChangeEvent : UnityEvent<float> { }

        [Serializable]
        public class DamageTakenEvent : UnityEvent<float> { } 

        void Awake()
        {
            animator = GetComponent<Animator>();
            baseStats = GetComponent<BaseStats>();
            maxHealth = new LazyValue<float>(GetStartingHealth);
        }

        void OnEnable()
        {
            onHealthChange.AddListener(HandleHeathUpdate);
            baseStats.onLevelChange += HandleLevelChange;
        }

        void Start()
        {
            SetStartingHealth();
        }

        void OnDisable()
        {
            onHealthChange.RemoveListener(HandleHeathUpdate);
            baseStats.onLevelChange -= HandleLevelChange;
        }

        public void TakeDamage(float amount, GameObject attacker)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);
            onHealthChange?.Invoke(currentHealth);
            onDamageTaken?.Invoke(amount);

            this.attacker = attacker;

        }

        public bool AddHealth(float amount)
        {
            if (currentHealth < maxHealth.value)
            {
                float healAmount = Mathf.Min(maxHealth.value - currentHealth, amount);
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

            maxHealth.value = baseStats.GetStat(Stat.Health);
            currentHealth = restoredHealth;

            onHealthChange?.Invoke(currentHealth);
        }

        float GetStartingHealth()
        {
            return baseStats.GetStat(Stat.Health);
        }

        void SetStartingHealth()
        {
            maxHealth.ForceInit();

            if (currentHealth < 0)
            {
                currentHealth = maxHealth.value;
            }
            
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
            maxHealth.value = baseStats.GetStat(Stat.Health);
            currentHealth = maxHealth.value;

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

