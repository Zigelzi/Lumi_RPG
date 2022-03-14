using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] [Range(0, 100f)] float chaseDistance = 5f;

        ActionScheduler actionScheduler;
        Attacking attacking;
        GameObject player;
        Health health;
        Vector3 spawnPosition;
        Quaternion spawnDirection;
        UnitMovement movement;

        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            attacking = GetComponent<Attacking>();
            movement = GetComponent<UnitMovement>();
            health = GetComponent<Health>();

            health.OnUnitDeath += HandleDeath;

            player = GameObject.FindGameObjectWithTag("Player");
            spawnPosition = transform.position;
            spawnDirection = transform.rotation;
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
                ReturnToSpawnPosition();
            }
        }

        void HandleDeath()
        {
            enabled = false;
            actionScheduler.CancelCurrentAction();
            movement.DisableNavAgent();
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

        void ReturnToSpawnPosition()
        {
            float distanceFromSpawnPosition = Vector3.Distance(transform.position, spawnPosition);

            movement.StartMovementAction(spawnPosition);

            if (distanceFromSpawnPosition <= 0.2f)
            {
                transform.rotation = spawnDirection;
            }
        }

        /*
         * Debugging methods 
         */
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}