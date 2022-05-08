using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] Transform leftHandHoldingLocation = null;
        [SerializeField] Transform rightHandHoldingLocation = null;
        [SerializeField] Weapon currentWeapon = null;
        [SerializeField] string defaultWeaponName = "Unarmed";

        GameObject currentWeaponInstance = null;        

        public Transform LeftHandHoldingLocation { get { return leftHandHoldingLocation; } }
        public Transform RightHandHoldingLocation { get { return rightHandHoldingLocation; } }
        public Weapon CurrentWeapon { get {  return currentWeapon; } }

        void Start()
        {
            Weapon defaultWeapon = Resources.Load<Weapon>(defaultWeaponName);
            if (defaultWeapon == null) return;

            EquipWeapon(defaultWeapon);    
        }

        public void EquipWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            if (CanSpawnWeapon())
            {

                DestroyCurrentWeapon();
                currentWeaponInstance = weapon.Spawn(leftHandHoldingLocation, rightHandHoldingLocation);
                weapon.SetAttackAnimation(animator);

                currentWeapon = weapon;
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

        void DestroyCurrentWeapon()
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeaponInstance);
                currentWeapon = null;
            }
        }
    }
}

