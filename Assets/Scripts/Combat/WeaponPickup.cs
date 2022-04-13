using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                WeaponManager weaponManager = other.GetComponent<WeaponManager>();
                weaponManager.EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}

