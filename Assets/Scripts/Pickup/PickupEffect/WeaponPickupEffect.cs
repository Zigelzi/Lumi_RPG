using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;

namespace RPG.Pickup
{
    [CreateAssetMenu(fileName = "PickupEffect_Weapon_", menuName = "Pickups/Pickup effects/Weapon", order = 0)]
    public class WeaponPickupEffect : PickupEffectStrategy
    {
        [SerializeField] WeaponConfig weaponConfig;

        public override bool GrantEffect(PickupData data)
        {
            if (weaponConfig == null) return false;

            if (data.User.TryGetComponent<WeaponManager>(out WeaponManager weaponManager))
            {
                weaponManager.EquipWeapon(weaponConfig);
                return true;
            }
            return false;
        }
    }
}
