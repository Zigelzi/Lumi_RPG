using System.Collections;
using UnityEngine;

namespace RPG.Pickup
{
    public class PickupData
    {
        GameObject user;

        public GameObject User { get { return user; } }

        public PickupData(GameObject newUser)
        {
            this.user = newUser;
        }
    }
}