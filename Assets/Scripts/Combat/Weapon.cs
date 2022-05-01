using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController attackAnimation;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] Projectile projectile;
        [SerializeField] WeaponType type;

        [SerializeField] [Range(0, 100f)] float attackDamage = 20f;
        [SerializeField] float attackRange = 2f;
        [SerializeField] [Range(0, 3f)] float attackSpeed = 1f;
        [SerializeField] bool isRightHanded = true;

        public bool HasProjectile { get { return projectile != null; } }
        
        public enum WeaponType
        {
            Unarmed,
            Sword,
            Bow
        }

        public WeaponType Type { get { return type; } }
        public float AttackDamage { get { return attackDamage; } }
        public float AttackRange { get { return attackRange; } }
        public float AttackSpeed { get { return attackSpeed; } }

        public void Spawn(Transform leftHand, Transform rightHand)
        {
            Transform handTransform = GetHandTransform(leftHand, rightHand);
            
            // TODO: Fix setting animation
            if (equippedPrefab != null)
            {
                Instantiate(equippedPrefab, handTransform);
            }
        }

        public void SetAttackAnimation(Animator animator)
        {
            if (attackAnimation != null)
            {
                animator.runtimeAnimatorController = attackAnimation;
            }
        }

        public void LaunchProjectile(Transform leftHand,
            Transform rightHand,
            Health target)
        {
            Transform handTransform = GetHandTransform(leftHand, rightHand);
            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);

            projectileInstance.SetTarget(target);
        }

        Transform GetHandTransform(Transform leftHand, Transform rightHand)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }

            return handTransform;
        }

    }
}
