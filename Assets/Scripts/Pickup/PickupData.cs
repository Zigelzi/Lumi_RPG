using System.Collections;
using UnityEngine;

namespace RPG.Pickup
{
    public class PickupData
    {
        GameObject user;
        GameObject gameObject;

        public GameObject User { get { return user; } }
        public GameObject GameObject { get { return gameObject; } }

        public PickupData(GameObject newPickup, GameObject newUser)
        {
            this.gameObject = newPickup;
            this.user = newUser;
        }
    }
}