using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities {

    [CreateAssetMenu(fileName = "Targeting_Self_", menuName = "Abilities/Targeting/Self", order = 0)]
    public class SelfTargeting: TargetingStrategy
    {
        public override void StartTargeting(AbilityData data, Action onTargetingFinished)
        {
            // Convert the single owner to array so that it can be processed as target
            GameObject[] target = new GameObject[] { data.GetUser() };
            data.SetTargets(target);
            onTargetingFinished();
        }

        public override void StopTargeting(GameObject user)
        {
            Debug.Log("Self targeting stopped");
        }
    }
}
