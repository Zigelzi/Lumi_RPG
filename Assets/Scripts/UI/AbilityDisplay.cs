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
        AbilityManager abilityManager;

        void Awake()
        {
            abilityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
        }
        void Start()
        {
            AddAbilities();
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
    }
}
