using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Effect_Projectile_", menuName = "Abilities/Effects/Projectile", order = 0)]
    public class ProjectileEffect : EffectStrategy
    {
        [SerializeField] float damageAmount = 10f;
        [SerializeField] Projectile projectilePrefab;
        public override void StartEffect(AbilityData data, Action onEffectFinished)
        {
            SpawnProjectile(data);
            onEffectFinished();
        }

        void SpawnProjectile(AbilityData data)
        {
            if (projectilePrefab == null) return;

            Transform castPoint = data.GetProjectileCastpoint();
            Projectile projectile = Instantiate(projectilePrefab, castPoint.position, castPoint.rotation);

            projectile.SetDamage(damageAmount);
            projectile.SetProjectileOwner(data.GetUser());
        }
    }
}
