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

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(int amount)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);

            if (currentHealth == 0)
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
    }
}

