using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicsControlToggler : MonoBehaviour
    {
        ActionScheduler actionScheduler;
        PlayerController player;
        PlayableDirector director;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            actionScheduler = player.GetComponent<ActionScheduler>();

            director = GetComponent<PlayableDirector>();

            director.played += DisablePlayerControl;
            director.stopped += EnablePlayerControl;
        }

        void OnDestroy()
        {
            director.played -= DisablePlayerControl;
            director.stopped -= EnablePlayerControl;
        }

        void DisablePlayerControl(PlayableDirector pd)
        {
            actionScheduler.CancelCurrentAction();
            player.enabled = false;
        }

        void EnablePlayerControl(PlayableDirector pd)
        {
            player.enabled = true;
        }
    }
}

