using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SFX
{
    public class SoundRandomizer : MonoBehaviour
    {
        [SerializeField][Range(-3, 3)] float pitchMin = 0;
        [SerializeField][Range(-3, 3)] float pitchMax = 1f;

        public void RandomisePitch(AudioSource audioSource)
        {
            if (audioSource == null) return;

            if (pitchMin < pitchMax)
            {
                float pitch = Random.Range(pitchMin, pitchMax);
                audioSource.pitch = pitch;
            }
        }
    }
}
