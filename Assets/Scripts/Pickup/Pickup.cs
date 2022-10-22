using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] PickupConfig pickUpConfig;
        private void OnTriggerEnter(Collider other)
        {
            if (pickUpConfig == null) return;
            if (!other.CompareTag("Player")) return;

            pickUpConfig.Use(gameObject, other.gameObject);
        }
    }
}
