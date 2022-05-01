﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] Transform leftHandHoldingLocation = null;
        [SerializeField] Transform rightHandHoldingLocation = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Weapon currentWeapon = null;

        public Transform LeftHandHoldingLocation { get { return leftHandHoldingLocation; } }
        public Transform RightHandHoldingLocation { get { return rightHandHoldingLocation; } }
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
                weapon.Spawn(leftHandHoldingLocation, rightHandHoldingLocation);
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
    }
}

