using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Create New Ability", order = 0)]
    public class Ability : ScriptableObject
    {
        [SerializeField] float cooldown = 2f;
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilterStrategy[] filterStrategies;
        [SerializeField] EffectStrategy[] effectStrategies;

        public float Cooldown { get { return cooldown; } }

        public void Use(GameObject user)
        {

            if (targetingStrategy != null) return;

            // Use lambda to provide TargetAquired context about it's user
            targetingStrategy.StartTargeting(user,
                (IEnumerable <GameObject> targets) => TargetAquired(user, targets));
            //SpawnVFX(castPoint);

        }

        public void Cancel(GameObject user)
        {
            if (HasStrategies())
            {
                targetingStrategy.StopTargeting(user);
            }
        }

        void TargetAquired(GameObject user, IEnumerable<GameObject> targets)
        {
            if (targets == null) return;

            IEnumerable<GameObject> filteredTargets = ApplyFilters(targets);
            StartEffects(user, filteredTargets);

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

        void StartEffects(GameObject user, IEnumerable<GameObject> targets)
        {
            foreach (EffectStrategy effect in effectStrategies)
            {
                if (effect == null) return;
                effect.StartEffect(user, targets, EffectFinished);
            }
        }

        void EffectFinished()
        {

        }

    }
}
