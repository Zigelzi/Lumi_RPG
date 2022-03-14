using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] [Range(0, 100f)] float chaseDistance = 5f;

        Attacking attacking;
        GameObject player;

        void Start()
        {
            attacking = GetComponent<Attacking>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if (IsPlayerInChaseRange())
            {
                Chase();
            }
        }

        bool IsPlayerInChaseRange()
        {
            if (player == null) return false;

            return Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;
        }

        void Chase()
        {
            attacking.StartAttackAction(player);
        }
    }
}