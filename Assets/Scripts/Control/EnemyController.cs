using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] [Range(0, 100f)] float chaseDistance = 5f;

        Attacking attacking;
        GameObject player;
        Health health;
        Vector3 spawnPosition;
        UnitMovement movement;

        void Start()
        {
            attacking = GetComponent<Attacking>();
            movement = GetComponent<UnitMovement>();
            health = GetComponent<Health>();

            health.OnUnitDeath += HandleDeath;

            player = GameObject.FindGameObjectWithTag("Player");
            spawnPosition = transform.position;
        }

        void OnDestroy()
        {
            health.OnUnitDeath -= HandleDeath;
        }

        void Update()
        {
            if (IsPlayerInChaseRange())
            {
                Chase();
            }
            else
            {
                attacking.Cancel();
                movement.MoveTo(spawnPosition);
            }
        }

        void HandleDeath()
        {
            enabled = false;
            attacking.Cancel();
            movement.Cancel();
        }

        bool IsPlayerInChaseRange()
        {
            if (player == null) return false;

            float distanceToPlayer = Vector3.Distance(
                transform.position,
                player.transform.position
                );
            return distanceToPlayer <= chaseDistance;
        }

        void Chase()
        {
            attacking.StartAttackAction(player);
        }
    }
}