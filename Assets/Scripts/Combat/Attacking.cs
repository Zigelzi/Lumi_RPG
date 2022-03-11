using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Control;
using RPG.Movement;

namespace RPG.Combat
{
    public class Attacking : MonoBehaviour, IAction
    {
        [SerializeField] float attackRange = 2f;

        ActionScheduler actionScheduler;
        UnitMovement movement;
        Transform target;

        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            movement = GetComponent<UnitMovement>();    
        }

        void Update()
        {
            if (target)
            {
                if (IsInAttackRange())
                {
                    movement.Cancel();
                    transform.LookAt(target.position);
                }
                else
                {
                    movement.MoveTo(target.position);
                }

            }    
        }
        public void Attack(EnemyController enemy)
        {
            actionScheduler.StartAction(this);
            target = enemy.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        bool IsInAttackRange()
        {
            float distanceFromTarget = Vector3.Distance(transform.position, target.position);
            if (distanceFromTarget <= attackRange)
            {
                return true;
            }

            return false;
        }
    }
}

