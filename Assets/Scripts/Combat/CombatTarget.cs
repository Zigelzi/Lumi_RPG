using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;
using RPG.Control;
using RPG.Movement;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController player, RaycastHit hit)
        {
            Attacking attacking = player.GetComponent<Attacking>();
            UnitMovement movement = player.GetComponent<UnitMovement>();

            if (attacking.IsInAttackRange(transform) || movement.CanMoveTo(transform.position))
            {
                player.TryStartAttackAction(this.gameObject);
                return true;
            }

            return false;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }
    }
}
