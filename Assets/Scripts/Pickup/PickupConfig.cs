using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup {
    [CreateAssetMenu(fileName = "Pickup_", menuName = "Pickups/New pickup", order = 0)]
    public class PickupConfig : ScriptableObject
    {
        [SerializeField] PickupEffectStrategy pickupEffectStrategy;
        [SerializeField] ConsumptionStrategy consumptionStrategy;
        public bool TryPickup(GameObject pickup, GameObject user)
        {
            PickupData data = new PickupData(pickup, user);
            if (pickupEffectStrategy == null) return false; 

            if (pickupEffectStrategy.GrantEffect(data))
            {
                if (consumptionStrategy == null) return false;

                consumptionStrategy.Consume(data);
                return true;
            }

            return false;

        }
    }
}
