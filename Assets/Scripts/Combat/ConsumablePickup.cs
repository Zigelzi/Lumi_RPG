using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

using RPG.Control;
using RPG.SFX;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class ConsumablePickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] ConsumableType consumableType = ConsumableType.Health;
        [SerializeField][Range(0, 100)] int amount = 20;

        public UnityEvent onPickup;

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

        public bool HandleRaycast(PlayerController player, RaycastHit hit)
        {
            player.TryStartMoveAction(hit.point);
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Interactable;
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

