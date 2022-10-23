using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup {
    [CreateAssetMenu(fileName = "Pickup_", menuName = "Pickups/New pickup", order = 0)]
    public class PickupConfig : ScriptableObject
    {
        [SerializeField] string pickupName = "";
        [SerializeField] PickupEffectStrategy pickupEffectStrategy;
        [SerializeField] ConsumptionStrategy consumptionStrategy;
        [SerializeField] FloatingText floatingTextPrefab;

        public bool TryPickup(PickupData data)
        {
            if (pickupEffectStrategy == null) return false; 

            if (pickupEffectStrategy.GrantEffect(data))
            {
                if (consumptionStrategy == null) return false;

                SpawnFloatingText(data);
                consumptionStrategy.Consume(data);
                return true;
            }

            return false;

        }

        void SpawnFloatingText(PickupData data)
        {
            FloatingText floatingTextInstance;
            CapsuleCollider collider = data.User.GetComponent<CapsuleCollider>();
            float yPositionOverCharacter = data.User.transform.position.y + collider.height;
            Vector3 spawnPositionAboveCharacter = new Vector3(data.User.transform.position.x,
                yPositionOverCharacter,
                data.User.transform.position.z);

            floatingTextInstance = Instantiate<FloatingText>(floatingTextPrefab,
                spawnPositionAboveCharacter, 
                data.User.transform.rotation,
                data.User.gameObject.transform);
            string text = $"{pickupName} +{data.Value}";

            if (floatingTextInstance == null) return;

            floatingTextInstance.SetText(text);
        }
    }
}
