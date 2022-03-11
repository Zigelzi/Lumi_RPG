using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class UnitMovement : MonoBehaviour, IAction
    {
        ActionScheduler actionScheduler;
        Animator animator;
        NavMeshAgent navAgent;

        // Start is called before the first frame update
        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMovementAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navAgent.SetDestination(destination);
            navAgent.isStopped = false;
        }

        public void Cancel()
        {
            navAgent.isStopped = true;
        }

        void UpdateAnimator()
        {
            Vector3 playerVelocity = navAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(playerVelocity);
            float forwardSpeed = localVelocity.z;

            // String reference defined in the character animator
            animator.SetFloat("forwardSpeed", forwardSpeed);

        }
    }
}
