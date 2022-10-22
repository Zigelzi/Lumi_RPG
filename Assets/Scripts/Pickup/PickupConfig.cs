using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup {
    [CreateAssetMenu(fileName = "Pickup_", menuName = "Pickups/New pickup", order = 0)]
    public class PickupConfig : ScriptableObject
    {
        [SerializeField] PickupEffectStrategy pickupEffectStrategy;
        [SerializeField] ConsumptionStrategy consumptionStrategy;
        public void Use(GameObject pickup, GameObject user)
        {
            PickupData data = new PickupData(pickup, user);
            if (pickupEffectStrategy == null) return; 

            if (pickupEffectStrategy.GrantEffect(data))
            {
                if (consumptionStrategy == null) return;

                consumptionStrategy.Consume(data);
            }

        }
    }
}
