using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;
using System;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Attacking : MonoBehaviour, IAction
    {
        ActionScheduler actionScheduler;
        Animator animator;
        BaseStats baseStats;
        WeaponConfig currentWeapon;
        GameObject currentTarget;
        UnitMovement movement;
        WeaponManager weaponManager;

        float timeSinceLastAttack = Mathf.Infinity;

        void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            movement = GetComponent<UnitMovement>();
            weaponManager = GetComponent<WeaponManager>();
            baseStats = GetComponent<BaseStats>();    
        }

        void OnEnable()
        {
            if (weaponManager != null)
            {
                weaponManager.onWeaponChange += HandleWeaponChange;
            }
        }

        void Start()
        {
            if (weaponManager != null)
            {
                currentWeapon = weaponManager.CurrentWeapon;
            }
        }

        void OnDisable()
        {
            if (weaponManager != null)
            {
                weaponManager.onWeaponChange -= HandleWeaponChange;
            }
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

        public void StartAttackAction(GameObject enemy)
        {
            actionScheduler.StartAction(this);
            currentTarget = enemy;
        }
        public void Cancel()
        {
            currentTarget = null;

            StopAttackAnimation();
            movement.Cancel();
        }

        void HandleWeaponChange(WeaponConfig newWeapon)
        {
            currentWeapon = newWeapon;
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

            if (!IsTargetAlive())
            {
                Cancel();
                timeSinceLastAttack = 0;
            }
        }

        bool IsInAttackRange()
        {
            float distanceFromTarget = Vector3.Distance(
                transform.position,
                currentTarget.transform.position
                );

            if (distanceFromTarget <= currentWeapon.AttackRange)
            {
                return true;
            }

            return false;
        }

        bool IsAbleToAttackAgain()
        {
            if (timeSinceLastAttack >= currentWeapon.AttackSpeed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTargetAlive()
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

        void Attack(GameObject enemy)
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

        // Triggered by attacking animation event "Shoot"
        void Shoot()
        {
            Hit();
        }

        // Triggered by attacking animation event "Hit"
        void Hit()
        {
            if (currentTarget == null) return;

            float damage = baseStats.GetStat(Stat.Damage);

            if (currentTarget.TryGetComponent<Health>(out Health currentTargetHealth))
            {
                if (currentWeapon.HasProjectile)
                {
                    currentWeapon.LaunchProjectile(weaponManager.LeftHandHoldingLocation, 
                        weaponManager.RightHandHoldingLocation,
                        currentTargetHealth,
                        gameObject,
                        damage);
                }
                else
                {
                    currentTargetHealth.TakeDamage(damage, gameObject);
                }
                
            }
        }
    }
}

