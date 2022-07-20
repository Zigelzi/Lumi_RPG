using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Attributes;

namespace RPG.SFX
{
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] AudioClip soundEffect;
        [SerializeField] bool isRandomized = false;

        AudioSource audioSource;
        SoundRandomizer soundRandomiser;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            soundRandomiser = GetComponent<SoundRandomizer>();
        }

        public void Play()
        {
            if (!IsDamageSFXPlayable()) return;

            if (isRandomized)
            {
                soundRandomiser.RandomisePitch(audioSource);
            }
            audioSource.clip = soundEffect;
            audioSource.Play();
        }

        bool IsDamageSFXPlayable()
        {
            if (audioSource != null || 
                soundEffect != null) return true;

            return false;
        }
    }
}
