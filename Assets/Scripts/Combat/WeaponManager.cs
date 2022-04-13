using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] Transform weaponHoldingLocation = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Weapon currentWeapon = null;

        public Weapon CurrentWeapon { get {  return currentWeapon; } }

        void Start()
        {
            EquipWeapon(defaultWeapon);    
        }

        public void EquipWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            if (CanSpawnWeapon())
            {
                weapon.Spawn(weaponHoldingLocation);
                weapon.SetAttackAnimation(animator);

                currentWeapon = weapon;
            }
        }

        bool CanSpawnWeapon()
        {
            if (weaponHoldingLocation != null)
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

