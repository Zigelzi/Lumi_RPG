using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup
{
    [CreateAssetMenu(fileName = "PickupEffect_Health_", menuName = "Pickups/Pickup effects/Health", order = 0)]
    public class PickupEffectHealth : PickupEffectStrategy
    {
        [SerializeField] float healAmount = 20f;

        public override bool GrantEffect(PickupData data)
        {
            if (data.User.TryGetComponent<Health>(out Health health))
            {
                return health.AddHealth(healAmount);
            }
            return false;
        }
    }
}
