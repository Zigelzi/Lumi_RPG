using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Effect_SpawnVFXAtCastpoint_", menuName = "Abilities/Effects/Spawn VFX at castpoint", order = 0)]
    public class SpawnVFXToCharacterEffect : EffectStrategy
    {
        [SerializeField] GameObject vfx;
        public override void StartEffect(AbilityData data, Action onEffectFinished)
        {
            SpawnVFX(data);
            onEffectFinished();
        }

        void SpawnVFX(AbilityData data)
        {
            Transform castPoint = data.GetCharacterCastpoint();
            GameObject user = data.GetUser();

            if (vfx == null) return;

            Instantiate(vfx, castPoint.position, castPoint.rotation, user.transform);
        }
    }
}
