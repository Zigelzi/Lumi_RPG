using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Effect_SingleTargetMelee_", menuName = "Abilities/Effects/Single target melee", order = 0)]
    public class SingleTargetMeleeEffect : EffectStrategy
    {
        [SerializeField] EffectStrategy[] appliedEffectsOnRange;
        public override void StartEffect(AbilityData data, Action onEffectFinished)
        {
            Attacking attacking = data.GetUser().GetComponent<Attacking>();


            foreach (GameObject target in data.GetTargets())
            {
                attacking.CurrentTarget = target;
                break;
            }
            data.StartCoroutine(WaitUntilAttackRange(data, onEffectFinished));
        }

        IEnumerator WaitUntilAttackRange(AbilityData data, Action onEffectFinished)
        {
            Attacking attacking = data.GetUser().GetComponent<Attacking>();

            while (!attacking.IsInAttackRange())
            {
                yield return null;
            }

            foreach (EffectStrategy effect in appliedEffectsOnRange)
            {
                effect.StartEffect(data, onEffectFinished);
            }
            yield break;
        }

        
    }
}