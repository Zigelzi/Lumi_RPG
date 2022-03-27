using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;
        [SerializeField] float currentHealth;
        [SerializeField] [Range(0, 30f)] float despawnTime = 10f; 

        Animator animator;
        bool isAlive = true;

        public bool IsAlive { get { return isAlive; } }

        public event Action OnUnitDeath;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);

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

