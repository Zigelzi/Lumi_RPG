using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] float combatDuration = 3f;
        [SerializeField] float durationInCombatRemaining = 0;

        Attacking attacking;
        Health health;

        void Awake()
        {
            attacking = GetComponent<Attacking>();
            health = GetComponent<Health>();
        }

        void OnEnable()
        {
            attacking.onAttackHit.AddListener(HandleCombatEvent);
            health.onDamageTaken.AddListener(HandleCombatEvent);
        }

        void Update()
        {
            ReduceCombatTimer();
        }

        void OnDisable()
        {
            health.onDamageTaken.RemoveListener(HandleCombatEvent);
        }

        public bool IsInCombat()
        {
            return durationInCombatRemaining > 0;
        }

        public void TriggerCombat()
        {
            durationInCombatRemaining = combatDuration;
        }

        void HandleCombatEvent()
        {
            durationInCombatRemaining = combatDuration;
        }

        void HandleCombatEvent(float amount)
        {
            durationInCombatRemaining = combatDuration;
        }

        void ReduceCombatTimer()
        {
            if (durationInCombatRemaining > 0)
            {
                durationInCombatRemaining -= Time.deltaTime;
            }
            else
            {
                durationInCombatRemaining = 0;
            }
        }
    }

}