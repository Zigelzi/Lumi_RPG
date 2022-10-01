using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] Ability[] abilities;

        Abilities.Ability currentAbility;

        public Abilities.Ability CurrentAbility { get { return currentAbility; } }

        // Start is called before the first frame update
        void Awake()
        {
            currentAbility = abilities[0].abilityConfig;
        }

        [System.Serializable]
        public class Ability
        {
            public int id;
            public Abilities.Ability abilityConfig;
        }
    }
}

