using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup
{
    [CreateAssetMenu(fileName = "Consumption_Respawn_", menuName = "Pickups/Consumption/Respawn", order = 0)]
    public class RespawnConsumption : ConsumptionStrategy
    {
        [SerializeField] float respawnDuration = 2f;
        public override void Consume(PickupData data)
        {
            if (data.GameObject == null) return;

            data.StartCoroutineOnPickup(HideForSeconds(data));
        }

        IEnumerator HideForSeconds(PickupData data)
        {
            ShowPickup(false, data);
            yield return new WaitForSeconds(respawnDuration);
            ShowPickup(true, data);
        }

        void ShowPickup(bool isVisible, PickupData data)
        {
            Collider collider = data.GameObject.GetComponent<Collider>();

            foreach (Transform child in data.GameObject.transform)
            {
                child.gameObject.SetActive(isVisible);
            }
            collider.enabled = isVisible;
        }
    }
}