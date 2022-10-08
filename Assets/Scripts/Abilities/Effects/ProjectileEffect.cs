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
        [SerializeField] GameObject projectilePrefab;
        public override void StartEffect(AbilityData data, Action onEffectFinished)
        {
            SpawnProjectile(data);
        }

        void SpawnProjectile(AbilityData data)
        {
            if (projectilePrefab == null) return;

            Transform castPoint = data.GetProjectileCastpoint();
            GameObject projectileInstance = Instantiate(projectilePrefab, castPoint.position, castPoint.rotation);
            Projectile projectile = projectileInstance.GetComponent<Projectile>();

            projectile.SetDamage(damageAmount);
            projectile.SetProjectileOwner(data.GetUser());
        }
    }
}
