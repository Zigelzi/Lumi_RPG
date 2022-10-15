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
        [SerializeField] Transform castPointProjectile;
        [SerializeField] Transform castPointCharacter;

        AbilityManager abilityManager;
        Ability currentAbility;
        ActionScheduler actionScheduler;
        Attunement attunement;
        CooldownStore cooldownStore;
        Health health;

        bool isTargeting = false;

        public bool IsTargeting { get { return isTargeting; } set { isTargeting = value; } }

        void Awake()
        {
            abilityManager = GetComponent<AbilityManager>();
            actionScheduler = GetComponent<ActionScheduler>();
            attunement = GetComponent<Attunement>();
            cooldownStore = GetComponent<CooldownStore>();
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

        public void StartCastingAction(int inputKey)
        {
            currentAbility = abilityManager.SelectAbility(inputKey);

            if (IsAbleToCastCurrentAbility())
            {
                actionScheduler.StartAction(this);
                currentAbility.Use(gameObject);                    
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

        bool IsAbleToCastCurrentAbility()
        {
            if (cooldownStore.IsAbilityReady(currentAbility) &&
                castPointProjectile != null &&
                castPointCharacter != null &&
                attunement.HasRequiredAttunement(currentAbility.AttunementCost)) 
            {
                return true;
            }

            return false;
        }
    }
}

