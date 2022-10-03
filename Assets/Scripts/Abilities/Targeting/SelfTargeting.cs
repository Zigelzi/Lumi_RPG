using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities {

    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Targeting/Create self targeting strategy", order = 0)]
    public class SelfTargeting: TargetingStrategy
    {
        public override void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> targetingFinished)
        {
            throw new System.NotImplementedException();
        }

        public override void StopTargeting(GameObject user)
        {
            Debug.Log("Self targeting stopped");
        }
    }
}
