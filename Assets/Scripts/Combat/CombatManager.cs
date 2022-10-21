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

        Health health;

        void Awake()
        {
            health = GetComponent<Health>();
        }

        void OnEnable()
        {
            health.onDamageTaken.AddListener(HandleDamageTaken);
        }

        void Update()
        {
            ReduceCombatTimer();
        }

        void OnDisable()
        {
            health.onDamageTaken.RemoveListener(HandleDamageTaken);
        }

        public bool IsInCombat()
        {
            return durationInCombatRemaining > 0;
        }

        void HandleDamageTaken(float amount)
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