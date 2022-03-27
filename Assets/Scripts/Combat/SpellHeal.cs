using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Core;

namespace RPG.Combat
{
    public class SpellHeal : MonoBehaviour
    {
        [SerializeField] float healAmount = 50f;
        [SerializeField] float cooldownTime = 2f;
        float timeSinceLastUsage = Mathf.Infinity;
        Health health;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
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

        void Heal()
        {
            health.AddHealth(healAmount);
            timeSinceLastUsage = 0;
        }

        bool IsSpellReady()
        {
            if (timeSinceLastUsage >= cooldownTime)
            {
                return true;
            }

            return false;
        }
    }
}

