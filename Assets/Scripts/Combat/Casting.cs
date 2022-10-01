using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using RPG.Abilities;
using RPG.Attributes;

namespace RPG.Combat
{
    public class Casting : MonoBehaviour
    {
        [SerializeField] Transform castPoint;

        float timeSinceLastUsage = Mathf.Infinity;
        AbilityManager abilityManager;
        Ability currentAbility;
        Health health;

        void Awake()
        {
            abilityManager = GetComponent<AbilityManager>();
            health = GetComponent<Health>();
        }

        void Start()
        {
            if (abilityManager == null) return;

            currentAbility = abilityManager.CurrentAbility;
        }

        void OnEnable()
        {
            health.onUnitDeath.AddListener(Disable);
        }

        void OnDisable()
        {
            health.onUnitDeath.RemoveListener(Disable);
        }

        void Update()
        {
            timeSinceLastUsage += Time.deltaTime;

            if (Keyboard.current.digit1Key.wasReleasedThisFrame && 
                IsAbilityReady() &&
                castPoint != null)
            {
                currentAbility.Use(health, castPoint);
            }
        }

        public void Disable()
        {
            enabled = false;
        }

        bool IsAbilityReady()
        {
            if (timeSinceLastUsage >= currentAbility.Cooldown)
            {
                return true;
            }

            return false;
        }
    }
}

