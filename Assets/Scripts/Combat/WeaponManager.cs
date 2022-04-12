using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform weaponHoldingLocation = null;
        [SerializeField] Weapon currentWeapon = null;

        // Start is called before the first frame update
        void Start()
        {
            SpawnWeapon();
        }

        void SetAttackAnimation(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();

            if (weapon.AttackAnimation == null) return;

            if (weapon.Type == Weapon.WeaponType.Sword)
            {
                animator.runtimeAnimatorController = weapon.AttackAnimation;
            }
        }

        void SpawnWeapon()
        {
            
            if (CanSpawnWeapon())
            {
                Weapon spawnedWeapon = Instantiate(weaponPrefab, weaponHoldingLocation)
                    .GetComponent<Weapon>();

                currentWeapon = spawnedWeapon;
                SetAttackAnimation(spawnedWeapon);
            }
            
        }

        bool CanSpawnWeapon()
        {
            if (weaponPrefab != null && weaponHoldingLocation != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

