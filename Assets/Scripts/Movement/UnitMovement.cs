using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class UnitMovement : MonoBehaviour
    {
        Animator animator;
        NavMeshAgent navAgent;

        // Start is called before the first frame update
        void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }


        public void MoveTo(Vector3 destination)
        {
            navAgent.SetDestination(destination);
            navAgent.isStopped = false;
        }

        public void Stop()
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
