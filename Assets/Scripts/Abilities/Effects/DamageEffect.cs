using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Effect_Damage_", menuName = "Abilities/Effects/Damage", order = 0)]
    public class DamageEffect : EffectStrategy
    {
        [SerializeField] float damageAmount = 10f;
        public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action onEffectFinished)
        {
            foreach(GameObject target in targets)
            {
                DealDamage(user, target);

            }
        }

        void DealDamage(GameObject user, GameObject target)
        {
            if (target.TryGetComponent<Health>(out Health targetHealth))
            {
                targetHealth.TakeDamage(damageAmount, user);
            }
        }
    }
}
