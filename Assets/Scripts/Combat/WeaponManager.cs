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
        [SerializeField] WeaponConfig defaultWeaponConfig = null;

        GameObject currentWeaponInstance;
        WeaponConfig currentWeaponConfig;
        LazyValue<Weapon> currentWeapon;
        
        public Transform LeftHandHoldingLocation { get { return leftHandHoldingLocation; } }
        public Transform RightHandHoldingLocation { get { return rightHandHoldingLocation; } }
        public WeaponConfig CurrentWeaponConfig { get {  return currentWeaponConfig; } }
        public Weapon CurrentWeapon { get { return currentWeapon.value; } }

        public event Action<WeaponConfig, Weapon> onWeaponChange;

        void Awake()
        {
            currentWeaponConfig = defaultWeaponConfig;
            currentWeapon = new LazyValue<Weapon>(SetDefaultWeapon);
        }

        void Start()
        {
            currentWeapon.ForceInit();
        }

        public void EquipWeapon(WeaponConfig weaponConfig)
        {
            SpawnWeapon(weaponConfig);
        }

        public IEnumerable<float> GetAdditiveModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeaponConfig.AttackDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeaponConfig.AttackMultiplier;
            }
        }

        Weapon SetDefaultWeapon()
        {
            Weapon spawnedWeapon = SpawnWeapon(defaultWeaponConfig);
            return spawnedWeapon;
        }

        Weapon SpawnWeapon(WeaponConfig weaponConfig)
        {
            Animator animator = GetComponent<Animator>();
            Weapon spawnedWeapon = null;

            if (CanSpawnWeapon())
            {
                DestroyCurrentWeapon();
                spawnedWeapon = weaponConfig.Spawn(leftHandHoldingLocation, rightHandHoldingLocation);
                if (spawnedWeapon != null)
                {
                    currentWeaponInstance = spawnedWeapon.gameObject;
                }
                weaponConfig.SetAttackAnimation(animator);

                currentWeaponConfig = weaponConfig;
                currentWeapon.value = spawnedWeapon;

                onWeaponChange?.Invoke(currentWeaponConfig, spawnedWeapon);
            }

            return spawnedWeapon;
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

        void DestroyCurrentWeapon()
        {
            if (currentWeaponInstance != null)
            {
                Destroy(currentWeaponInstance);
                currentWeaponConfig = null;
            }
        }

        public object CaptureState()
        {
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string restoredWeaponName = (string)state;
            WeaponConfig restoredWeapon = Resources.Load<WeaponConfig>(restoredWeaponName);
            EquipWeapon(restoredWeapon);
        }
    }
}

