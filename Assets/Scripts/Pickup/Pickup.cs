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


        private void OnTriggerEnter(Collider other)
        {
            if (pickUpConfig == null) return;
            if (!other.CompareTag("Player")) return;

            bool isConsumed = pickUpConfig.TryPickup(gameObject, other.gameObject);

            if (isConsumed)
            {
                onPickup?.Invoke();
            }
        }
    }
}
