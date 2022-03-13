using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Attacking : MonoBehaviour, IAction
    {
        [SerializeField] float attackRange = 2f;
        [SerializeField][Range(0, 3f)] float attackSpeed = 1f;
        [SerializeField] int attackDamage = 20;
        [SerializeField] ParticleSystem attackParticles;

        ActionScheduler actionScheduler;
        Animator animator;
        CombatTarget currentEnemy;
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

        public void StartAttackAction(CombatTarget enemy)
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
            if (IsInAttackRange() && 
                IsAbleToAttackAgain() &&
                IsTargetAlive())
            {
                Attack(currentEnemy);
                timeSinceLastAttack = 0;
            }
        }

        void Attack(CombatTarget enemy)
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

        bool IsTargetAlive()
        {
            Health currentEnemyHealth = currentEnemy.GetComponent<Health>();

            if (currentEnemyHealth == null) return false;

            if (currentEnemyHealth.IsAlive)
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
            // This will trigger Hit() event
            animator.SetTrigger("attack");
        }

        // Triggered by attacking animation event "Hit"
        void Hit()
        {
            if (target == null) return;

            if (target.TryGetComponent<Health>(out Health targetHealth))
            {
                targetHealth.TakeDamage(attackDamage);
                PlayAttackParticleEffect();
            }
        }

        void PlayAttackParticleEffect()
        {
            if (attackParticles == null) return;

            attackParticles.Play();
        }

    }
}

