using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.SFX;

namespace RPG.Attributes
{
    public class DamageSound : MonoBehaviour
    {
        [SerializeField] AudioClip damageSFX;
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
        }

        void OnDisable()
        {
            health.onDamageTaken -= HandleDamageTaken;    
        }

        void HandleDamageTaken(float amount)
        {
            if (audioSource == null || damageSFX == null) return;

            soundRandomiser.RandomisePitch(audioSource);
            audioSource.clip = damageSFX;
            audioSource.Play();
        }
    }
}
