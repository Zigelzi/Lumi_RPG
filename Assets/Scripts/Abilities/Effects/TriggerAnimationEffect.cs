using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Effect_TriggerAnimation_", menuName = "Abilities/Effects/Trigger animation", order = 0)]
    public class TriggerAnimationEffect : EffectStrategy
    {
        [SerializeField] AnimationTrigger animationTrigger;

        // List of triggers in characted animator
        public enum AnimationTrigger
        {
            useOffensiveAbility,
            useBuffAbility
        }
        public override void StartEffect(AbilityData data, Action onEffectFinished)
        {
            TriggerAnimation(data);
            onEffectFinished();
        }

        void TriggerAnimation(AbilityData data)
        {
            if (data.GetUser().TryGetComponent<Animator>(out Animator animator))
            {
                animator.SetTrigger(animationTrigger.ToString());
            }
        }
    }

}
