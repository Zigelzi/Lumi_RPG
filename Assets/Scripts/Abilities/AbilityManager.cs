using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Control;
using RPG.Attributes;

namespace RPG.Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] float healAmount = 50f;
        [SerializeField] GameObject spellParticles;
        [SerializeField] Ability[] abilities;

        Abilities.Ability currentAbility;

        Health health;
        PlayerController player;

        public Abilities.Ability CurrentAbility { get { return currentAbility; } }

        // Start is called before the first frame update
        void Awake()
        {
            health = GetComponent<Health>();
            player = GetComponent<PlayerController>();

            currentAbility = abilities[0].abilityConfig;
        }


        //void Heal()
        //{
        //    bool isHealed = health.AddHealth(healAmount);

        //    if (isHealed)
        //    {
        //        SpawnHealParticles();
        //    }
            
        //}

        //void SpawnHealParticles()
        //{
        //    if (spellParticles == null && player != null) return;

        //    //Transform spellCastingPoint = player.SpellCastingPoint;
        //    //Instantiate(spellParticles, spellCastingPoint.position, Quaternion.identity, transform);

        //}

        [System.Serializable]
        public class Ability
        {
            public int id;
            public Abilities.Ability abilityConfig;
        }
    }
}

