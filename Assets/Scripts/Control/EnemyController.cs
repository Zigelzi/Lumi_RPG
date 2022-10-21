using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameDevTV.Utils;

using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] [Range(0, 100f)] float aggrevationRadius = 5f;
        [SerializeField] [Range(0, 20f)] float suspiciousDuration = 3f;
        [SerializeField][Range(0, 20f)] float aggrevatedDuration = 3f;
        [SerializeField][Range(0, 20f)] float alertRadius = 3f;
        [SerializeField] [Range(0, 10f)] float patrolStopDuration = 1f;
        [SerializeField] [Range(0, 1f)] float patrolSpeedMultiplier = 0.5f;
        [SerializeField] float waypointTolerance = 0.3f;
        [SerializeField] PatrolPath patrolPath;

        ActionScheduler actionScheduler;
        Attacking attacking;
        CapsuleCollider enemyCollider;
        CombatManager combatManager;
        GameObject player;
        Health health;
        LazyValue<Vector3> guardPosition;
        UnitMovement movement;

        int currentWaypointIndex = 0;
        
        float timeLastSawPlayer = Mathf.Infinity;
        float timeAtWaypoint = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            actionScheduler = GetComponent<ActionScheduler>();
            attacking = GetComponent<Attacking>();
            combatManager = player.GetComponent<CombatManager>();
            enemyCollider = GetComponent<CapsuleCollider>();
            movement = GetComponent<UnitMovement>();
            health = GetComponent<Health>();

            guardPosition = new LazyValue<Vector3>(GetStartingPosition);
        }

        void OnEnable()
        {
            health.onUnitDeath.AddListener(HandleDeath);
            health.onDamageTaken.AddListener(HandleDamageTaken);
        }

        void Start()
        {
            guardPosition.ForceInit();
        }

        void OnDisable()
        {
            health.onUnitDeath.RemoveListener(HandleDeath);
            health.onDamageTaken.RemoveListener(HandleDamageTaken);
        }

        void Update()
        {
            if (IsAggrevated())
            {
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

        public void Aggrevate()
        {
            timeSinceAggrevated = 0;
        }

        void HandleDeath()
        {
            actionScheduler.CancelCurrentAction();
            movement.SetNavAgent(false);
            enemyCollider.enabled = false;

            enabled = false;
        }

        void HandleDamageTaken(float amount)
        {
            timeSinceAggrevated = 0;
            AggrevateNearbyEnemies();
        }

        Vector3 GetStartingPosition()
        {
            return transform.position;
        }

        bool IsAggrevated()
        {
            return IsPlayerInAggrevationRange() || IsStillAggrevated();
        }

        bool IsPlayerInAggrevationRange()
        {
            if (player == null) return false;

            float distanceToPlayer = Vector3.Distance(
                transform.position,
                player.transform.position
                );
            return distanceToPlayer <= aggrevationRadius;
        }

        bool IsStillAggrevated()
        {
            return timeSinceAggrevated <= aggrevatedDuration;
        }

        bool IsStillSuspicious()
        {
            return timeLastSawPlayer <= suspiciousDuration;
        }

        void AttackBehaviour()
        {
            if (IsPlayerInAggrevationRange())
            {
                combatManager.TriggerCombat();

                timeLastSawPlayer = 0;
            }
            attacking.StartAttackAction(player);
        }

        void AggrevateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, alertRadius, Vector3.up, 0);

            foreach(RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
                {
                    enemy.Aggrevate();
                }
            }
        }

        void SuspiciousBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        void GuardBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;

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
            timeSinceAggrevated += Time.deltaTime;
        }

        /*
         * Debugging methods 
         */
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, aggrevationRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, alertRadius);
        }
    }
}