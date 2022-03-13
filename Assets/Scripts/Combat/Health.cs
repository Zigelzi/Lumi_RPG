using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealth = 100;
        [SerializeField] int currentHealth;

        public event Action OnUnitDeath;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(int amount)
        {
            if (currentHealth > 0)
            {
                currentHealth -= amount;
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Destroy(gameObject);
            OnUnitDeath?.Invoke();
        }
    }
}

