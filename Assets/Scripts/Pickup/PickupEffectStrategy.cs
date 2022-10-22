using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup
{
    public abstract class PickupEffectStrategy : ScriptableObject
    {
        public abstract bool GrantEffect(PickupData data);
    }
}
