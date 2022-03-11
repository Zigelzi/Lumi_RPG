using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class Attacking : MonoBehaviour
    {
        public void Attack(EnemyController target)
        {
            Debug.Log($"Attacking target {target.gameObject.name}!");
        }
    }
}

