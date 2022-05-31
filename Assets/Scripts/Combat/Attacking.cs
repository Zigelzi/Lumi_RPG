﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;

namespace RPG.Combat
{
    public class Attacking : MonoBehaviour, IAction
    {
        ActionScheduler actionScheduler;
        Animator animator;
        BaseStats baseStats;
        GameObject currentTarget;
        UnitMovement movement;
        WeaponManager weaponManager;

        float timeSinceLastAttack = Mathf.Infinity;
        float baseDamage = 10f;

        void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            movement = GetComponent<UnitMovement>();
            weaponManager = GetComponent<WeaponManager>();
            baseStats = GetComponent<BaseStats>();

            if (baseStats == null) return;
            baseDamage = baseStats.GetStartingStat(Stat.Damage);

            baseStats.onLevelChange += HandleLevelChange;
        }

        void OnDestroy()
        {
            baseStats.onLevelChange -= HandleLevelChange;
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

        void HandleLevelChange(int newLevel)
        {
            baseDamage = baseStats.GetStat(Stat.Damage, newLevel);
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

            Weapon currentWeapon = weaponManager.CurrentWeapon;
            if (distanceFromTarget <= currentWeapon.AttackRange)
            {
                return true;
            }

            return false;
        }

        bool IsAbleToAttackAgain()
        {
            Weapon currentWeapon = weaponManager.CurrentWeapon;

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

            Weapon currentWeapon = weaponManager.CurrentWeapon;
            if (currentTarget.TryGetComponent<Health>(out Health currentTargetHealth))
            {
                if (currentWeapon.HasProjectile)
                {
                    currentWeapon.LaunchProjectile(weaponManager.LeftHandHoldingLocation, 
                        weaponManager.RightHandHoldingLocation,
                        currentTargetHealth,
                        gameObject,
                        baseDamage);
                }
                else
                {
                    DealMeleeDamage(currentWeapon, currentTargetHealth);
                }
                
            }
        }

        void DealMeleeDamage(Weapon weapon,Health currentTarget)
        {
            float totalDamage = baseDamage + weapon.AttackDamage;
            currentTarget.TakeDamage(totalDamage, gameObject);
        }

    }
}

