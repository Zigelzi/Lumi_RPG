using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Create New Ability", order = 0)]
    public class AbilityConfig : ScriptableObject
    {
        [SerializeField] GameObject vfx;
        [SerializeField] float cooldown = 2f;

        public float Cooldown { get { return cooldown; } }

        public void Cast(Health target, Transform castPoint)
        {
            Debug.Log($"Casted ability at {target.gameObject.name}!");
            SpawnVFX(castPoint);

        }

        void SpawnVFX(Transform castPoint)
        {
            if (vfx == null) return;

            Instantiate(vfx, castPoint.position, castPoint.rotation, castPoint);
        }

        

    }
}
