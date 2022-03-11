using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Movement;

namespace RPG.Combat
{
    public class Attacking : MonoBehaviour
    {
        [SerializeField] float attackRange = 2f;

        UnitMovement movement;
        Transform target;

        void Start()
        {
            movement = GetComponent<UnitMovement>();    
        }

        void Update()
        {
            if (target)
            {
                if (IsInAttackRange())
                {
                    movement.Stop();
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
            target = enemy.transform;
        }

        public void ClearTarget()
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

