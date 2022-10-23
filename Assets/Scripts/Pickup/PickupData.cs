using System.Collections;
using UnityEngine;

namespace RPG.Pickup
{
    public class PickupData
    {
        GameObject user;
        GameObject gameObject;
        float value;

        public GameObject User { get { return user; } }
        public GameObject GameObject { get { return gameObject; } }

        public float Value { get { return value; } set { this.value = value; } }

        public PickupData(GameObject newPickup, GameObject newUser)
        {
            this.gameObject = newPickup;
            this.user = newUser;
        }

        public void StartCoroutineOnPickup(IEnumerator coroutine)
        {
            if (gameObject == null) return;

            gameObject.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
        }
    }
}