using System.Collections;
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
        LazyValue<Weapon> currentWeapon;
        [SerializeField] Weapon defaultWeapon = null;

        GameObject currentWeaponInstance = null;
        
        public Transform LeftHandHoldingLocation { get { return leftHandHoldingLocation; } }
        public Transform RightHandHoldingLocation { get { return rightHandHoldingLocation; } }
        public Weapon CurrentWeapon { get {  return currentWeapon.value; } }

        public event Action<Weapon> onWeaponChange;

        void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetDefaultWeapon);
        }

        void Start()
        {
            currentWeapon.ForceInit();
        }

        public void EquipWeapon(Weapon weapon)
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

        Weapon SetDefaultWeapon()
        {
            SpawnWeapon(defaultWeapon);
            return defaultWeapon;
        }

        void SpawnWeapon(Weapon weapon)
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
            Weapon restoredWeapon = Resources.Load<Weapon>(restoredWeaponName);
            EquipWeapon(restoredWeapon);
        }
    }
}

