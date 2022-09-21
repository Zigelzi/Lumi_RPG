using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class DestroyFXAfterEffect : MonoBehaviour
    {
        List<ParticleSystem> childParticles = new List<ParticleSystem>();
        List<ParticleSystem> aliveParticles = new List<ParticleSystem>();

        void Awake()
        {
            GetChildParticles();
            StartCoroutine(DestroyFinishedFXWithChildren(0));
        }

        void GetChildParticles()
        {
            ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem particle in particles)
            {
                childParticles.Add(particle);
                aliveParticles.Add(particle);
            }
        }

        IEnumerator DestroyFinishedFXWithChildren(float waitTime) { 
            while (aliveParticles.Count > 0)
            {
                foreach (ParticleSystem vfx in childParticles)
                {
                    DisableFinishedParticle(vfx);
                }
                yield return new WaitForSeconds(waitTime);
            }
            Destroy(gameObject);
        }

        void DisableFinishedParticle(ParticleSystem vfx)
        {
            if (!vfx.IsAlive())
            {
                vfx.gameObject.SetActive(false);
                aliveParticles.Remove(vfx);
            }
        }

        
    }
}

