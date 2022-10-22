using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup
{
    [CreateAssetMenu(fileName = "Consumption_Destroy_", menuName = "Pickups/Consumption/Destroy", order = 0)]
    public class DestroyConsumption : ConsumptionStrategy
    {
        [SerializeField] float destroyDelay = 0f; 
        public override void Consume(PickupData data)
        {
            if (data.GameObject == null) return;

            Destroy(data.GameObject, destroyDelay);
        }
    }
}
