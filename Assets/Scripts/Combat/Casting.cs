﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Control;
using RPG.Core;

namespace RPG.Combat
{
    public class Casting : MonoBehaviour
    {
        [SerializeField] float healAmount = 50f;
        [SerializeField] float cooldownTime = 2f;
        [SerializeField] GameObject spellParticles;

        float timeSinceLastUsage = Mathf.Infinity;
        Health health;
        PlayerController player;

        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
            player = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastUsage += Time.deltaTime;

            if (Keyboard.current.digit1Key.wasReleasedThisFrame && IsSpellReady())
            {
                Heal();
            }
        }
        bool IsSpellReady()
        {
            if (timeSinceLastUsage >= cooldownTime)
            {
                return true;
            }

            return false;
        }

        void Heal()
        {
            bool isHealed = health.AddHealth(healAmount);

            if (isHealed)
            {
                SpawnHealParticles();
                timeSinceLastUsage = 0;
            }
            
        }

        void SpawnHealParticles()
        {
            if (spellParticles == null && player != null) return;

            Transform spellCastingPoint = player.SpellCastingPoint;
            Instantiate(spellParticles, spellCastingPoint.position, spellCastingPoint.rotation);

        }

        
    }
}
