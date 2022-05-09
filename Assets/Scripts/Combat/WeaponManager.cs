using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Combat
{
    public class WeaponManager : MonoBehaviour, ISaveable
    {
        [SerializeField] Transform leftHandHoldingLocation = null;
        [SerializeField] Transform rightHandHoldingLocation = null;
        [SerializeField] Weapon currentWeapon = null;
        [SerializeField] Weapon defaultWeapon = null;

        GameObject currentWeaponInstance = null;
        

        public Transform LeftHandHoldingLocation { get { return leftHandHoldingLocation; } }
        public Transform RightHandHoldingLocation { get { return rightHandHoldingLocation; } }
        public Weapon CurrentWeapon { get {  return currentWeapon; } }

        void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            } 
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
                //Debug.Log($"{gameObject.name} equipped weapon {currentWeapon.name}");
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

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string restoredWeaponName = (string)state;
            Weapon restoredWeapon = Resources.Load<Weapon>(restoredWeaponName);
            Debug.Log($"Restored weapon {restoredWeapon.name} to {gameObject.name}");
            EquipWeapon(restoredWeapon);
        }
    }
}

