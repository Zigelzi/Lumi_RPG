using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Combat;
using RPG.Saving;

namespace RPG.Movement
{
    public class UnitMovement : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 3f;
        [SerializeField] float maxPathLength = 20f;

        ActionScheduler actionScheduler;
        Animator animator;
        Casting casting;
        NavMeshAgent navAgent;

        // Start is called before the first frame update
        void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            casting = GetComponent<Casting>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateRunAnimation();
        }

        public void StartMovementAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);

            if (casting != null && casting.IsTargeting)
            {
                casting.Cancel();
            }
            
            MoveTo(destination);
        }

        public void StartMovementAction(Vector3 destination, float speedMultiplier)
        {
            actionScheduler.StartAction(this);

            if (casting != null && casting.IsTargeting)
            {
                casting.Cancel();
            }
            MoveTo(destination, speedMultiplier);
        }
        public void MoveTo(Vector3 destination)
        {
            if (CanMoveTo(destination))
            {
                navAgent.speed = maxSpeed;
                navAgent.SetDestination(destination);
                navAgent.isStopped = false;
            }
        }

        public void MoveTo(Vector3 destination, float speedMultiplier)
        {
            if (CanMoveTo(destination))
            {
                // Safeguard to keep the multiplier between 0 and 1
                speedMultiplier = Mathf.Clamp01(speedMultiplier);

                navAgent.speed = maxSpeed * speedMultiplier;
                navAgent.SetDestination(destination);
                navAgent.isStopped = false;
            }
        }

        public void Cancel()
        {
            navAgent.isStopped = true;
        }

        public void SetNavAgent(bool isActive)
        {
            navAgent.enabled = isActive;
        }

        public bool CanMoveTo(Vector3 targetPosition)
        {
            NavMeshPath path = new NavMeshPath();

            bool isPathFound = NavMesh.CalculatePath(
                transform.position,
                targetPosition,
                NavMesh.AllAreas,
                path);

            float pathLength = GetPathLength(path);

            if (isPathFound
                && pathLength <= maxPathLength
                && path.status == NavMeshPathStatus.PathComplete)
            {
                return true;
            }

            return false;
        }

        float GetPathLength(NavMeshPath path)
        {
            float totalPathLength = 0;

            if (path.corners.Length < 2) return totalPathLength;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                totalPathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return totalPathLength;
        }

        void UpdateRunAnimation()
        {
            Vector3 playerVelocity = navAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(playerVelocity);
            float forwardSpeed = localVelocity.z;

            // String reference defined in the character animator
            animator.SetFloat("forwardSpeed", forwardSpeed);

        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 restoredPosition = (SerializableVector3)state;

            navAgent.Warp(restoredPosition.ToVector());
            
        }
    }
}
