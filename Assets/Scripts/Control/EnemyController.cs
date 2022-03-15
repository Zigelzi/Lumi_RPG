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
        [SerializeField] [Range(0, 20f)] float suspiciousDuration = 3f;

        ActionScheduler actionScheduler;
        Attacking attacking;
        GameObject player;
        Health health;
        Vector3 spawnPosition;
        Quaternion spawnDirection;
        UnitMovement movement;

        
        float timeLastSawPlayer = Mathf.Infinity;

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
            if (IsPlayerInAggroRange())
            {
                timeLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (IsStillSuspicious()) {
                SuspiciousBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            timeLastSawPlayer += Time.deltaTime;
        }

        void HandleDeath()
        {
            enabled = false;
            actionScheduler.CancelCurrentAction();
            movement.DisableNavAgent();
        }

        bool IsPlayerInAggroRange()
        {
            if (player == null) return false;

            float distanceToPlayer = Vector3.Distance(
                transform.position,
                player.transform.position
                );
            return distanceToPlayer <= chaseDistance;
        }

        bool IsStillSuspicious()
        {
            return timeLastSawPlayer <= suspiciousDuration;
        }

        void AttackBehaviour()
        {
            attacking.StartAttackAction(player);
        }

        void SuspiciousBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        void GuardBehaviour()
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