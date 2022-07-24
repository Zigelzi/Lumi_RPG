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
        LazyValue<WeaponConfig> currentWeaponConfig;
        [SerializeField] WeaponConfig defaultWeapon = null;

        Weapon currentWeapon = null;
        
        public Transform LeftHandHoldingLocation { get { return leftHandHoldingLocation; } }
        public Transform RightHandHoldingLocation { get { return rightHandHoldingLocation; } }
        public WeaponConfig CurrentWeapon { get {  return currentWeaponConfig.value; } }

        public event Action<WeaponConfig> onWeaponChange;

        void Awake()
        {
            currentWeaponConfig = new LazyValue<WeaponConfig>(SetDefaultWeapon);
        }

        void Start()
        {
            currentWeaponConfig.ForceInit();
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            SpawnWeapon(weapon);
            onWeaponChange?.Invoke(currentWeaponConfig.value);
        }

        public IEnumerable<float> GetAdditiveModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeaponConfig.value.AttackDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeaponConfig.value.AttackMultiplier;
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
                currentWeapon = weapon.Spawn(leftHandHoldingLocation, rightHandHoldingLocation);
                currentWeaponConfig.value = weapon;
                weapon.SetAttackAnimation(animator);
            }
            
        }

        void DestroyCurrentWeapon()
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
                currentWeaponConfig.value = null;
            }
        }

        public object CaptureState()
        {
            return currentWeaponConfig.value.name;
        }

        public void RestoreState(object state)
        {
            string restoredWeaponName = (string)state;
            WeaponConfig restoredWeapon = Resources.Load<WeaponConfig>(restoredWeaponName);
            EquipWeapon(restoredWeapon);
        }
    }
}

