using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;
using RPG.Movement;

namespace RPG.Pickup
{
    public class Pickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] PickupConfig pickUpConfig;

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

            pickUpConfig.Use(gameObject, other.gameObject);
        }
    }
}
