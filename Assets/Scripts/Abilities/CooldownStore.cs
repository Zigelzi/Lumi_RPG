using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Abilities
{
    public class CooldownStore : MonoBehaviour
    {

        Dictionary<Ability, float> cooldownTimers = new Dictionary<Ability, float>();

        void Update()
        {
            DecreaseCooldowns();
        }

        public void StartCooldown(Ability ability, float duration)
        {
            cooldownTimers.Add(ability, duration);
        }

        public bool IsAbilityReady(Ability ability)
        {
            if (GetCooldownRemaining(ability) == 0)
            {
                return true;
            }

            return false;
        }

        public float GetCooldownRemaining(Ability ability)
        {
            if (cooldownTimers.ContainsKey(ability))
            {
                return cooldownTimers[ability];
            }

            return 0;
        }

        void DecreaseCooldowns()
        {
            List<Ability> abilities = new List<Ability>(cooldownTimers.Keys);

            foreach (Ability ability in abilities)
            {
                cooldownTimers[ability] -= Time.deltaTime;

                if (cooldownTimers[ability] < 0)
                {
                    cooldownTimers.Remove(ability);
                }
                
            }
        }
    }

}