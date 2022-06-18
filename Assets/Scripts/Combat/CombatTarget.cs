using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;
using RPG.Control;
using RPG.UI;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController player, RaycastHit hit)
        {
            player.TryStartAttackAction(this.gameObject);
            return true;
        }
    }
}
