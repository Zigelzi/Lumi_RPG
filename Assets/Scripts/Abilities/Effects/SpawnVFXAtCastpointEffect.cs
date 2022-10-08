using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Effect_SpawnVFXAtCastpoint_", menuName = "Abilities/Effects/Spawn VFX at castpoint", order = 0)]
    public class SpawnVFXAtCastpointEffect : EffectStrategy
    {
        [SerializeField] GameObject vfx;
        public override void StartEffect(AbilityData data, Action onEffectFinished)
        {
            onEffectFinished();
        }

        void SpawnVFX(AbilityData data)
        {
            if (vfx == null) return;


        }
    }
}
