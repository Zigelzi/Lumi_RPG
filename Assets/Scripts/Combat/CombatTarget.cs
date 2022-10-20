using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController player, RaycastHit hit)
        {
            Attacking attacking = player.GetComponent<Attacking>();

            if (attacking.IsInAttackRange(gameObject.transform))
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
