using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using RPG.Abilities;

namespace RPG.UI
{
    public class AbilityDisplay : MonoBehaviour
    {
        GameObject player;
        AbilityManager abilityManager;
        CooldownStore cooldownStore;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            abilityManager = player.GetComponent<AbilityManager>();
            cooldownStore = player.GetComponent<CooldownStore>();
        }
        void Start()
        {
            AddAbilities();
        }

        void Update()
        {
            UpdateAbilityCooldowns();
        }

        void AddAbilities()
        {
            TMP_Text[] textSlots = GetComponentsInChildren<TMP_Text>();

            for (int i = 0; i < textSlots.Length; i++)
            {
                Ability ability = abilityManager.Abilities[i].ability;
                
                textSlots[i].text = $"{i + 1} - {ability.AbilityName} ({ability.Cooldown} s CD)";
            }
        }

        void UpdateAbilityCooldowns()
        {
            TMP_Text[] textSlots = GetComponentsInChildren<TMP_Text>();
            for (int i = 0; i < textSlots.Length; i++)
            {
                Ability ability = abilityManager.Abilities[i].ability;
                string cooldownRemaining = cooldownStore.GetCooldownRemaining(ability).ToString("0.0");
                textSlots[i].text = $"{i + 1} - {ability.AbilityName} ({cooldownRemaining} / {ability.Cooldown} s CD)";
            }
        }
    }
}
