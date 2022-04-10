using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float currentHealth;
        [SerializeField] float maxHealth = 100f;
        [SerializeField] [Range(0, 30f)] float despawnTime = 10f; 

        Animator animator;
        bool isAlive = true;

        public float CurrentHealth { get { return currentHealth; } }
        public float MaxHealth { get { return maxHealth; } }
        public bool IsAlive { get { return isAlive; } }

        public event Action OnUnitDeath;
        public event Action<float> OnHealthChange;

        // Start is called before the first frame update
        void Awake()
        {
            animator = GetComponent<Animator>();
            OnHealthChange += HandleHeathUpdate;

            currentHealth = maxHealth;

        }

        void OnDestroy()
        {
            OnHealthChange -= HandleHeathUpdate;
        }

        public void TakeDamage(float amount)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);
            OnHealthChange?.Invoke(currentHealth);

        }

        public bool AddHealth(float amount)
        {
            if (currentHealth < maxHealth)
            {
                float healAmount = Mathf.Min(maxHealth - currentHealth, amount);
                currentHealth += healAmount;
                OnHealthChange?.Invoke(currentHealth);

                return true;
            }

            return false;
        }

        void HandleHeathUpdate(float newHealth)
        {
            if (newHealth == 0)
            {
                Die();
            }
        }

        void Die()
        {
            if (isAlive)
            {
                isAlive = false;
                animator.SetTrigger("die");
                OnUnitDeath?.Invoke();

                if (gameObject.tag != "Player")
                {
                    Invoke("Despawn", despawnTime);
                }
                
            } 
        }

        void Despawn()
        {
            Destroy(gameObject);
        }

        public object CaptureState()
        {
            if (gameObject.tag == "Player")
            {
                Debug.Log($"Saving player health to {currentHealth}");
            }
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            float restoredHealth = (float)state;

            currentHealth = restoredHealth;

            OnHealthChange?.Invoke(currentHealth);
        }
    }
}

