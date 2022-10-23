using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;
using RPG.Movement;
using UnityEngine.Events;

namespace RPG.Pickup
{
    public class Pickup : MonoBehaviour, IRaycastable
    {
        
        [SerializeField] PickupConfig pickUpConfig;

        public UnityEvent onPickup;

        void OnTriggerEnter(Collider other)
        {
            
            PickupData data;
            bool isConsumed;

            if (pickUpConfig == null) return;
            if (!other.CompareTag("Player")) return;

            data = new PickupData(gameObject, other.gameObject);
            isConsumed = pickUpConfig.TryPickup(data);

            if (isConsumed)
            {
                onPickup?.Invoke();
            }
        }

        public bool HandleRaycast(PlayerController player, RaycastHit hit)
        {
            UnitMovement movement = player.GetComponent<UnitMovement>();
            if (movement.CanMoveTo(hit.point))
            {
                player.TryStartMoveAction(hit.point);
                return true;
            }

            return false;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Interactable;
        }
       
    }
}
