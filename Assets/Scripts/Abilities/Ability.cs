using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Create New Ability", order = 0)]
    public class Ability : ScriptableObject
    {
        [SerializeField] GameObject vfx;
        [SerializeField] float cooldown = 2f;
        [SerializeField] TargetingStrategy targetingStrategy;

        public float Cooldown { get { return cooldown; } }



        public void Use(GameObject user, Transform castPoint)
        {
            Debug.Log($"Casted ability {this}!");
            targetingStrategy.StartTargeting(user);
            SpawnVFX(castPoint);

        }

        void SpawnVFX(Transform castPoint)
        {
            if (vfx == null) return;

            Instantiate(vfx, castPoint.position, castPoint.rotation, castPoint);
        }

        

    }
}
