using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform weaponHoldingLocation = null;
        // Start is called before the first frame update
        void Start()
        {
            SpawnWeapon();
        }

        void SpawnWeapon()
        {
            if (CanSpawnWeapon())
            {
                Instantiate(weaponPrefab, weaponHoldingLocation);
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

