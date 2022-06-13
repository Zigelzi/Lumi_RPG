using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{
    public class UnitMovement : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 3f;
        ActionScheduler actionScheduler;
        Animator animator;
        NavMeshAgent navAgent;

        // Start is called before the first frame update
        void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
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
            MoveTo(destination);
        }

        public void StartMovementAction(Vector3 destination, float speedMultiplier)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination, speedMultiplier);
        }
        public void MoveTo(Vector3 destination)
        {
            navAgent.speed = maxSpeed;
            navAgent.SetDestination(destination);
            navAgent.isStopped = false;
        }

        public void MoveTo(Vector3 destination, float speedMultiplier)
        {
            // Safeguard to keep the multiplier between 0 and 1
            speedMultiplier = Mathf.Clamp01(speedMultiplier);

            navAgent.speed = maxSpeed * speedMultiplier;
            navAgent.SetDestination(destination);
            navAgent.isStopped = false;
        }

        public void Cancel()
        {
            navAgent.isStopped = true;
        }

        public void SetNavAgent(bool isActive)
        {
            navAgent.enabled = isActive;
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
