using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public abstract class EffectStrategy : ScriptableObject
    {
        public abstract void StartTargeting(GameObject user);
        public abstract void StopTargeting(GameObject user);
    }
}
