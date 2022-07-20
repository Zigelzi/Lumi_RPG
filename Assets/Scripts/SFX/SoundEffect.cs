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
        [SerializeField] bool isPlayedOnStart = false;

        AudioSource audioSource;
        SoundRandomizer soundRandomiser;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            soundRandomiser = GetComponent<SoundRandomizer>();
        }

        void Start()
        {
           if (isPlayedOnStart)
            {
                Play();
            }    
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
