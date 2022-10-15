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
        [SerializeField] string abilityName;
        [SerializeField] float attunementCost = 5f;
        [SerializeField] float cooldown = 2f;
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilterStrategy[] filterStrategies;
        [SerializeField] EffectStrategy[] effectStrategies;

        public string AbilityName { get { return abilityName; } }
        public float AttunementCost { get { return attunementCost; } }
        public float Cooldown { get { return cooldown; } }

        public bool Use(GameObject user)
        {
            AbilityData data = new AbilityData(user);
            bool isUsed = false;
            if (targetingStrategy == null) return isUsed;

            // Use lambda to provide TargetAquired context about it's user
            targetingStrategy.StartTargeting(data, () => {
                isUsed = TargetAquired(data);
            });
            return isUsed;
        }

        public void Cancel(GameObject user)
        {
            if (HasStrategies())
            {
                targetingStrategy.StopTargeting(user);
            }
        }

        bool TargetAquired(AbilityData data)
        {
            if (data == null) return false;

            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();

            IEnumerable <GameObject> filteredTargets = ApplyFilters(data.GetTargets());
            data.SetTargets(filteredTargets);

            StartEffects(data);
            cooldownStore.StartCooldown(this, cooldown);
            return true;

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
