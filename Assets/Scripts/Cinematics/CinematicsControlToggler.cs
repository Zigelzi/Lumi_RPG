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
        PlayerController playerController;
        PlayableDirector director;

        // Start is called before the first frame update
        void Awake()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            actionScheduler = playerController.GetComponent<ActionScheduler>();

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
            playerController.enabled = false;
        }

        void EnablePlayerControl(PlayableDirector pd)
        {
            playerController.enabled = true;
        }
    }
}

