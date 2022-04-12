using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController attackAnimation;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] WeaponType type;

        [SerializeField] [Range(0, 100f)] float attackDamage = 20f;
        [SerializeField] float attackRange = 2f;
        [SerializeField] [Range(0, 3f)] float attackSpeed = 1f;
        
        public enum WeaponType
        {
            Unarmed,
            Sword
        }

        public WeaponType Type { get { return type; } }
        public float AttackDamage { get { return attackDamage; } }
        public float AttackRange { get { return attackRange; } }
        public float AttackSpeed { get { return attackSpeed; } }

        public void Spawn(Transform holdinPosition)
        {
            // TODO: Fix setting animation
            if (equippedPrefab != null)
            {
                Instantiate(equippedPrefab, holdinPosition);
            }
        }

        public void SetAttackAnimation(Animator animator)
        {
            if (attackAnimation != null)
            {
                animator.runtimeAnimatorController = attackAnimation;
            }
        }
    }
}
