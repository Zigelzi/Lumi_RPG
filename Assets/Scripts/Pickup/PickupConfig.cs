using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup {
    [CreateAssetMenu(fileName = "Pickup_", menuName = "Pickups/New pickup", order = 0)]
    public class PickupConfig : ScriptableObject
    {
        [SerializeField] PickupEffectStrategy pickupEffectStrategy;
        public void Use(GameObject user)
        {
            PickupData data = new PickupData(user);
            if (pickupEffectStrategy == null) return; 

            pickupEffectStrategy.GrantEffect(data);
        }
    }
}
