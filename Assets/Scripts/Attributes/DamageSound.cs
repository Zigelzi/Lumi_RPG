using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.SFX;

namespace RPG.Attributes
{
    public class DamageSound : MonoBehaviour
    {
        [SerializeField] AudioClip damageSFX;
        [SerializeField] AudioClip deathSFX;

        AudioSource audioSource;
        Health health;
        SoundRandomizer soundRandomiser;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            health = GetComponentInParent<Health>();
            soundRandomiser = GetComponent<SoundRandomizer>();
        }

        void OnEnable()
        {
            health.onDamageTaken += HandleDamageTaken;
            //health.onUnitDeath += HandleUnitDeath;
        }

        void OnDisable()
        {
            health.onDamageTaken -= HandleDamageTaken;
            //health.onUnitDeath -= HandleUnitDeath;
        }

        void HandleDamageTaken(float amount)
        {
            if (IsDamageSFXPlayable())
            {
                soundRandomiser.RandomisePitch(audioSource);
                audioSource.clip = damageSFX;
                audioSource.Play();
            }
        }

        bool IsDamageSFXPlayable()
        {
            if (audioSource != null || 
                damageSFX != null) return true;

            return false;
        }

        void HandleUnitDeath()
        {
            Debug.Log("Unit died");
            if (IsDeathSFXPlayable())
            {
                audioSource.clip = deathSFX;
                audioSource.Play();
            }
        }
        
        bool IsDeathSFXPlayable()
        {
            if (audioSource != null ||
                deathSFX != null) return true;

            return false;
        }
    }
}
