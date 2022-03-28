using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Core
{
    public class Health : MonoBehaviour
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
        void Start()
        {
            animator = GetComponent<Animator>();
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);
            OnHealthChange?.Invoke(currentHealth);

            if (currentHealth == 0)
            {
                Die();
            }
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
    }
}

