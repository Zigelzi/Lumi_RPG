using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    public class ConsumablePickup : MonoBehaviour
    {
        [SerializeField] ConsumableType consumableType = ConsumableType.Health;
        [SerializeField][Range(0, 100)] int amount = 20;

        enum ConsumableType
        {
            Health
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                ConsumePickup(other);
            }    
        }

        void ConsumePickup(Collider playerCollider)
        {
            if (consumableType == ConsumableType.Health)
            {
                Health playerHealth = playerCollider.GetComponent<Health>();

                if (playerHealth == null) return;

                Heal(playerHealth);
            }
        }

        void Heal(Health playerHealth)
        {
            bool isHealed = playerHealth.AddHealth(amount);

            if (isHealed)
            {
                Destroy(gameObject);
            }
        } 
    }
}

