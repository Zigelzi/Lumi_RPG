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
        WeaponConfig currentWeaponConfig;
        Weapon currentWeapon;
        [SerializeField] GameObject currentTarget;
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
                currentWeaponConfig = weaponManager.CurrentWeaponConfig;
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

        public bool IsInAttackRange(Transform targetTransform)
        {
            float distanceFromTarget = Vector3.Distance(
                transform.position,
                targetTransform.position
                );

            if (distanceFromTarget <= currentWeaponConfig.AttackRange)
            {
                return true;
            }

            return false;
        }

        void HandleWeaponChange(WeaponConfig newWeaponConfig, Weapon newWeapon)
        {
            currentWeaponConfig = newWeaponConfig;
            currentWeapon = newWeapon;
        }

        void UpdateLastAttackTime()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        void ChaseTarget()
        {
            if (IsInAttackRange(currentTarget.transform))
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
            if (IsInAttackRange(currentTarget.transform) && 
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

        

        bool IsAbleToAttackAgain()
        {
            if (timeSinceLastAttack >= currentWeaponConfig.AttackSpeed)
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

            if (currentWeapon != null)
            {
                currentWeapon.OnHit();
            }

            if (currentTarget.TryGetComponent<Health>(out Health currentTargetHealth))
            {
                if (currentWeaponConfig.HasProjectile)
                {
                    currentWeaponConfig.LaunchProjectile(weaponManager.LeftHandHoldingLocation, 
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

