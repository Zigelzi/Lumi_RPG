using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup
{
    public abstract class ConsumptionStrategy : ScriptableObject
    {
        public abstract void Consume(PickupData data);
    }
}
