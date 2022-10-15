using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] AbilityConfig[] abilities;

        Ability currentAbility;

        public AbilityConfig[] Abilities { get { return abilities; } }
        public Ability CurrentAbility { get { return currentAbility; } }

        void Awake()
        {
            currentAbility = abilities[0].ability;
        }

        public Ability SelectAbility(int inputKey)
        {
            // Input bindings start from 1 and abilities are zero indexed
            int abilityNumber = inputKey - 1;
            if (abilityNumber < abilities.Length)
            {
                currentAbility = abilities[abilityNumber].ability;
            }

            return currentAbility;
        }

        [System.Serializable]
        public class AbilityConfig
        {
            public int id;
            public Ability ability;
        }
    }
}

