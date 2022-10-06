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
        public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action onEffectFinished)
        {
            
            SpawnProjectile(user);
        }

        void SpawnProjectile(GameObject user)
        {
            if (projectilePrefab == null) return;

            Transform castPoint = user.GetComponentInChildren<CastPoint>().transform;
            GameObject projectileInstance = Instantiate(projectilePrefab, castPoint.position, castPoint.rotation);
            Projectile projectile = projectileInstance.GetComponent<Projectile>();

            projectile.SetDamage(damageAmount);
            projectile.SetProjectileOwner(user);
        }
    }
}
