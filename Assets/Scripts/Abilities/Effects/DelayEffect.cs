using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Effect_Delay_", menuName = "Abilities/Effects/Delay", order = 0)]
    public class DelayEffect : EffectStrategy
    {
        [SerializeField] float delay = 0f;
        [SerializeField] EffectStrategy[] delayedEffects;
        public override void StartEffect(AbilityData data, Action onEffectFinished)
        {
            data.StartCoroutine(DelayEffects(data, onEffectFinished));
            onEffectFinished();
        }

        IEnumerator DelayEffects(AbilityData data, Action onEffectFinished)
        {
            foreach(EffectStrategy effect in delayedEffects)
            {
                yield return new WaitForSeconds(delay);
                effect.StartEffect(data, onEffectFinished);
            }
        }
    }

}