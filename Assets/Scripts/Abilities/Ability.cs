using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Create New Ability", order = 0)]
    public class Ability : ScriptableObject
    {
        [SerializeField] GameObject vfx;
        [SerializeField] float cooldown = 2f;
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilterStrategy filterStrategy;

        public float Cooldown { get { return cooldown; } }

        public void Use(GameObject user, Transform castPoint)
        {
            if (HasStrategies())
            {
                targetingStrategy.StartTargeting(user, TargetAquired);
                SpawnVFX(castPoint);
            }
        }

        public void Cancel(GameObject user)
        {
            if (HasStrategies())
            {
                targetingStrategy.StopTargeting(user);
            }
        }

        void TargetAquired(IEnumerable<GameObject> targets)
        {
            if (targets == null) return;

            IEnumerable<GameObject> filteredTargets = filterStrategy.Filter(targets);
            
            foreach (GameObject target in filteredTargets)
            {
                Debug.Log($"Targeted :{target}");
            }
        }

        void SpawnVFX(Transform castPoint)
        {
            if (vfx == null) return;

            Instantiate(vfx, castPoint.position, castPoint.rotation);
        }

        bool HasStrategies()
        {
            if (targetingStrategy != null &&
                filterStrategy != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
