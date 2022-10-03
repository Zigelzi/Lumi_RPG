using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RPG.Control;

namespace RPG.UI
{
    public class GameOverCanvas : MonoBehaviour
    {
        
        void OnEnable()
        {
            PlayerController.onPlayerDeath += HandlePlayerDeath;
        }

        void Start()
        {
            DisplayGameOverUI(false);
        }

        void OnDisable()
        {
            PlayerController.onPlayerDeath -= HandlePlayerDeath;
        }

        void HandlePlayerDeath()
        {
            DisplayGameOverUI(true);
        }

        void DisplayGameOverUI(bool isVisible)
        {
            Canvas gameOverCanvas = GetComponent<Canvas>();
            gameOverCanvas.enabled = isVisible;
        }
    }
}
