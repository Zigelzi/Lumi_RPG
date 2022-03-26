using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicsTrigger : MonoBehaviour
    {
        bool isUnplayed = true;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && isUnplayed)
            {
                PlayableDirector director = GetComponent<PlayableDirector>();
                isUnplayed = false;
                director.Play();
            }
        }

    }
}

