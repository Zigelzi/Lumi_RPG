using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using System;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Create New Ability", order = 0)]
    public class Ability : ScriptableObject
    {
        [SerializeField] float cooldown = 2f;
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilterStrategy[] filterStrategies;
        [SerializeField] EffectStrategy[] effectStrategies;

        public void Use(GameObject user)
        {
            AbilityData data = new AbilityData(user);

            if (targetingStrategy == null) return;

            // Use lambda to provide TargetAquired context about it's user
            targetingStrategy.StartTargeting(data, () => TargetAquired(data));
        }

        public void Cancel(GameObject user)
        {
            if (HasStrategies())
            {
                targetingStrategy.StopTargeting(user);
            }
        }

        void TargetAquired(AbilityData data)
        {
            if (data == null) return;

            IEnumerable<GameObject> filteredTargets = ApplyFilters(data.GetTargets());
            data.SetTargets(filteredTargets);

            StartEffects(data);
            data.GetUser().GetComponent<CooldownStore>().StartCooldown(this, cooldown);

        }

        bool HasStrategies()
        {
            if (targetingStrategy != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        IEnumerable<GameObject> ApplyFilters(IEnumerable<GameObject> targets)
        {
            IEnumerable<GameObject> filteredTargets = targets;

            foreach (FilterStrategy filterStrategy in filterStrategies)
            {
                if (filterStrategy == null) continue;

                filteredTargets = filterStrategy.Filter(filteredTargets);
            }

            return filteredTargets;
        }

        void StartEffects(AbilityData data)
        {
            foreach (EffectStrategy effect in effectStrategies)
            {
                if (effect == null) return;
                effect.StartEffect(data, EffectFinished);
            }
        }

        void EffectFinished()
        {
            //throw new NotImplementedException();
        }

    }
}
