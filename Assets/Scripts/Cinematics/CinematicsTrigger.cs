using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicsTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            PlayableDirector director = GetComponent<PlayableDirector>();

            if (other.gameObject.tag == "Player")
            {
                director.Play();
            }
            
        }
    }
}

