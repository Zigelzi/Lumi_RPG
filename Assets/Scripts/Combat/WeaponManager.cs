﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

using GameDevTV.Utils;

using RPG.Stats;

namespace RPG.Combat
{
    public class WeaponManager : MonoBehaviour, ISaveable, IStatModifier
    {
        [SerializeField] Transform leftHandHoldingLocation = null;
        [SerializeField] Transform rightHandHoldingLocation = null;
        LazyValue<WeaponConfig> currentWeapon;
        [SerializeField] WeaponConfig defaultWeapon = null;

        GameObject currentWeaponInstance = null;
        
        public Transform LeftHandHoldingLocation { get { return leftHandHoldingLocation; } }
        public Transform RightHandHoldingLocation { get { return rightHandHoldingLocation; } }
        public WeaponConfig CurrentWeapon { get {  return currentWeapon.value; } }

        public event Action<WeaponConfig> onWeaponChange;

        void Awake()
        {
            currentWeapon = new LazyValue<WeaponConfig>(SetDefaultWeapon);
        }

        void Start()
        {
            currentWeapon.ForceInit();
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            SpawnWeapon(weapon);
            currentWeapon.value = weapon;
            onWeaponChange?.Invoke(currentWeapon.value);
        }

        public IEnumerable<float> GetAdditiveModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.AttackDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.AttackMultiplier;
            }
        }


        bool CanSpawnWeapon()
        {
            if (leftHandHoldingLocation != null || rightHandHoldingLocation != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        WeaponConfig SetDefaultWeapon()
        {
            SpawnWeapon(defaultWeapon);
            return defaultWeapon;
        }

        void SpawnWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();

            if (CanSpawnWeapon())
            {
                DestroyCurrentWeapon();
                currentWeaponInstance = weapon.Spawn(leftHandHoldingLocation, rightHandHoldingLocation);
                weapon.SetAttackAnimation(animator);
            }
            
        }

        void DestroyCurrentWeapon()
        {
            if (currentWeaponInstance != null)
            {
                Destroy(currentWeaponInstance);
                currentWeapon.value = null;
            }
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string restoredWeaponName = (string)state;
            WeaponConfig restoredWeapon = Resources.Load<WeaponConfig>(restoredWeaponName);
            EquipWeapon(restoredWeapon);
        }
    }
}

