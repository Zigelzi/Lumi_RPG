using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Targeting/Create targeting strategy", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting()
        {
            Debug.Log("Demo targeting started");
        }
    }
}
