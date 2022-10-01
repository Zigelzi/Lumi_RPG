using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities {

    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Targeting/Create self targeting strategy", order = 0)]
    public class SelfTargeting: TargetingStrategy
    {
        public override void StartTargeting(GameObject user)
        {
            throw new System.NotImplementedException();
        }
    }
}
