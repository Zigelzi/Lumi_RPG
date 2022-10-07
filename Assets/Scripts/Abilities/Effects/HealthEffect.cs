using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Effect_Health_", menuName = "Abilities/Effects/Health", order = 0)]
    public class HealthEffect : EffectStrategy
    {
        [Tooltip("Negative values deal damage and positive values heal")]
        [SerializeField] float healthChange = 10f;
        public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action onEffectFinished)
        {
            foreach(GameObject target in targets)
            {
                ApplyHealthEffect(user, target);
            }
            onEffectFinished();
        }

        void ApplyHealthEffect(GameObject user, GameObject target)
        {
            if (target.TryGetComponent<Health>(out Health targetHealth))
            {
                if (healthChange < 0)
                {
                    targetHealth.TakeDamage(-healthChange, user);
                }
                else
                {
                    targetHealth.AddHealth(healthChange);
                }
            }
        }
    }
}
