﻿using RPG.Attributes;
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
        public override void StartEffect(AbilityData data, Action onEffectFinished)
        {
            foreach(GameObject target in data.GetTargets())
            {
                ApplyHealthEffect(data, target);
            }
            onEffectFinished();
        }

        void ApplyHealthEffect(AbilityData data, GameObject target)
        {
            if (target.TryGetComponent<Health>(out Health targetHealth))
            {
                if (healthChange < 0)
                {
                    targetHealth.TakeDamage(-healthChange, data.GetUser());
                }
                else
                {
                    targetHealth.AddHealth(healthChange);
                }
            }
        }
    }
}