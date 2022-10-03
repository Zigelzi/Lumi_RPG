using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Self targeting", menuName = "Abilities/Targeting/Self", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> targetingFinished)
        {
            Debug.Log("Demo targeting started");
            targetingFinished(null);
        }

        public override void StopTargeting(GameObject user)
        {
            Debug.Log("Demo targeting stopped");
        }
    }
}
