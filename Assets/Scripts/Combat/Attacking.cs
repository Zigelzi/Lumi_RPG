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
        CombatTarget currentTarget;
        UnitMovement movement;

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
            if (currentTarget)
            {
                ChaseTarget();
                TryAttackTarget();
            }    
        }

        public void StartAttackAction(CombatTarget enemy)
        {
            actionScheduler.StartAction(this);
            currentTarget = enemy;
        }
        public void Cancel()
        {
            currentTarget = null;

            StopAttackAnimation();
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
                transform.LookAt(currentTarget.transform.position);
            }
            else
            {
                movement.MoveTo(currentTarget.transform.position);
            }
        }

        void TryAttackTarget()
        {
            if (IsInAttackRange() && 
                IsAbleToAttackAgain() &&
                IsTargetAlive())
            {
                Attack(currentTarget);
                timeSinceLastAttack = 0;
            }
        }

        bool IsInAttackRange()
        {
            float distanceFromTarget = Vector3.Distance(
                transform.position,
                currentTarget.transform.position
                );

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
            Health currentTargetHealth = currentTarget.GetComponent<Health>();

            if (currentTargetHealth == null) return false;

            if (currentTargetHealth.IsAlive)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        void Attack(CombatTarget enemy)
        {
            PlayAttackAnimation();
        }

        void PlayAttackAnimation()
        {
            // Reset trigger so the animation isn't canceled immediately if 
            // player has canceled the attacking previously
            animator.ResetTrigger("stopAttack");
            // This will trigger Hit() event
            animator.SetTrigger("attack");
        }

        void StopAttackAnimation()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        // Triggered by attacking animation event "Hit"
        void Hit()
        {
            if (currentTarget == null) return;

            if (currentTarget.TryGetComponent<Health>(out Health currentTargetHealth))
            {
                currentTargetHealth.TakeDamage(attackDamage);
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

