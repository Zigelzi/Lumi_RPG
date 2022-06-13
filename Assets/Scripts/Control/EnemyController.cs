using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] [Range(0, 100f)] float aggroRadius = 5f;
        [SerializeField] [Range(0, 20f)] float suspiciousDuration = 3f;
        [SerializeField] [Range(0, 10f)] float patrolStopDuration = 1f;
        [SerializeField] [Range(0, 1f)] float patrolSpeedMultiplier = 0.5f;
        [SerializeField] float waypointTolerance = 0.3f;
        [SerializeField] PatrolPath patrolPath;

        ActionScheduler actionScheduler;
        Attacking attacking;
        CapsuleCollider enemyCollider;
        GameObject player;
        Health health;
        Vector3 guardPosition;
        UnitMovement movement;

        int currentWaypointIndex = 0;
        
        float timeLastSawPlayer = Mathf.Infinity;
        float timeAtWaypoint = Mathf.Infinity;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            actionScheduler = GetComponent<ActionScheduler>();
            attacking = GetComponent<Attacking>();
            enemyCollider = GetComponent<CapsuleCollider>();
            movement = GetComponent<UnitMovement>();
            health = GetComponent<Health>();
        }

        void OnEnable()
        {
            health.onUnitDeath += HandleDeath;
        }

        void Start()
        {
            guardPosition = transform.position;
        }

        void OnDisable()
        {
            health.onUnitDeath -= HandleDeath;
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

            UpdateTimers();
        }

        void HandleDeath()
        {
            

            actionScheduler.CancelCurrentAction();
            movement.SetNavAgent(false);
            enemyCollider.enabled = false;

            enabled = false;
        }

        bool IsPlayerInAggroRange()
        {
            if (player == null) return false;

            float distanceToPlayer = Vector3.Distance(
                transform.position,
                player.transform.position
                );
            return distanceToPlayer <= aggroRadius;
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
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeAtWaypoint = 0;
                    SetNextWaypoint();
                }
                nextPosition = GetCurrentWaypointPosition();
            }

            if (!IsWaitingAtWaypoint())
            {
                movement.StartMovementAction(nextPosition, patrolSpeedMultiplier);
                
            }

        }

        private bool AtWaypoint()
        {
            float distanceFromWaypoint = Vector3.Distance(
                transform.position,
                GetCurrentWaypointPosition()
                );
            return distanceFromWaypoint < waypointTolerance;
        }

        void SetNextWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        Vector3 GetCurrentWaypointPosition()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        bool IsWaitingAtWaypoint()
        {
            return timeAtWaypoint < patrolStopDuration;
        }

        void UpdateTimers()
        {
            timeLastSawPlayer += Time.deltaTime;
            timeAtWaypoint += Time.deltaTime;
        }

        /*
         * Debugging methods 
         */
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, aggroRadius);
        }
    }
}