using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Self targeting", menuName = "Abilities/Targeting/Self", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting(GameObject user)
        {
            Debug.Log("Demo targeting started");
        }

        public override void StopTargeting(GameObject user)
        {
            Debug.Log("Demo targeting stopped");
        }
    }
}
