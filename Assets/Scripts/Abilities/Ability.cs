﻿using System.Collections;
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
            targetingStrategy.StartTargeting(user, TargetAquired);
            SpawnVFX(castPoint);

        }

        public void Cancel(GameObject user)
        {
            targetingStrategy.StopTargeting(user);
        }

        void TargetAquired(IEnumerable<GameObject> targets)
        {
            if (targets == null) return;

            foreach (GameObject target in targets)
            {
                Debug.Log($"Targeted :{target}");
            }
        }

        void SpawnVFX(Transform castPoint)
        {
            if (vfx == null) return;

            Instantiate(vfx, castPoint.position, castPoint.rotation);
        }

        

    }
}
