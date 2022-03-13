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
        [SerializeField][Range(0, 3f)] float attackSpeed = 1f;
        [SerializeField] int attackDamage = 20;

        ActionScheduler actionScheduler;
        Animator animator;
        EnemyController currentEnemy;
        UnitMovement movement;
        Transform target;

        float timeSinceLastAttack;

        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            movement = GetComponent<UnitMovement>();    
        }

        void Update()
        {
            UpdateLastAttackTime();
            if (target)
            {
                ChaseTarget();
                TryAttackTarget();
            }    
        }

        public void StartAttackAction(EnemyController enemy)
        {
            actionScheduler.StartAction(this);
            target = enemy.transform;
            currentEnemy = enemy;
        }
        public void Cancel()
        {
            target = null;
        }

        void UpdateLastAttackTime()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        void ChaseTarget()
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

        void TryAttackTarget()
        {
            if (IsInAttackRange() && IsAbleToAttackAgain())
            {
                Attack(currentEnemy);
                timeSinceLastAttack = 0;
            }
        }

        void Attack(EnemyController enemy)
        {
            PlayAttackAnimation();
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

        bool IsAbleToAttackAgain()
        {
            if (timeSinceLastAttack >= attackSpeed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void PlayAttackAnimation()
        {
            animator.SetTrigger("attack");
        }

        // Triggered by attacking animation event
        void Hit()
        {
            if (target == null) { return; }

            if (target.TryGetComponent<Health>(out Health targetHealth))
            {
                targetHealth.TakeDamage(attackDamage);
            }
        }
    }
}

