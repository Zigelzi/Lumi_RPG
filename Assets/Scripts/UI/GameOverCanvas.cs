using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

public class GameOverCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.onPlayerDeath += HandlePlayerDeath;

        DisplayGameOverUI(false);
    }

    void OnDestroy()
    {
        PlayerController.onPlayerDeath -= HandlePlayerDeath;
    }

    void HandlePlayerDeath()
    {
        DisplayGameOverUI(true);
    }

    void DisplayGameOverUI(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
