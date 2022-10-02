using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using RPG.Abilities;
using RPG.Attributes;
using RPG.Core;

namespace RPG.Combat
{
    public class Casting : MonoBehaviour, IAction
    {
        [SerializeField] Transform castPoint;

        AbilityManager abilityManager;
        Ability currentAbility;
        ActionScheduler actionScheduler;
        Health health;

        float timeSinceLastUsage = Mathf.Infinity;
        bool isTargeting = false;

        public bool IsTargeting { get { return isTargeting; } set { isTargeting = value; } }

        void Awake()
        {
            abilityManager = GetComponent<AbilityManager>();
            health = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();
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
        }

        public void StartCastingAction(int inputKey)
        {
            currentAbility = abilityManager.SelectAbility(inputKey);

            if (IsAbilityReady() &&
                castPoint != null)
            {
                actionScheduler.StartAction(this);
                currentAbility.Use(gameObject, castPoint);
            }
        }

        public void Disable()
        {
            enabled = false;
        }

        public void Cancel()
        {
            currentAbility.Cancel(gameObject);
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

