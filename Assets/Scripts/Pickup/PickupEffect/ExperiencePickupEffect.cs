using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Pickup
{
    [CreateAssetMenu(fileName = "PickupEffect_Experience_", menuName = "Pickups/Pickup effects/Experience", order = 0)]
    public class ExperiencePickupEffect : PickupEffectStrategy
    {
        [SerializeField] float experienceAmount = 20f;

        public override bool GrantEffect(PickupData data)
        {
            if (data.User.TryGetComponent<Experience>(out Experience experience))
            {
                experience.AddExperience(experienceAmount);
                data.Value = experienceAmount;
                return true;
            }
            return false;
        }
    }
}