using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] Transform weaponHoldingLocation = null;
        [SerializeField] Weapon currentWeapon = null;

        public Weapon CurrentWeapon { get {  return currentWeapon; } }

        // Start is called before the first frame update
        void Start()
        {
            SpawnWeapon();
        }

        void SpawnWeapon()
        {
            Animator animator = GetComponent<Animator>();
            if (CanSpawnWeapon())
            {
                currentWeapon.Spawn(weaponHoldingLocation);
                currentWeapon.SetAttackAnimation(animator);
            }

            
        }

        bool CanSpawnWeapon()
        {
            if (weaponHoldingLocation != null && currentWeapon != null)
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

