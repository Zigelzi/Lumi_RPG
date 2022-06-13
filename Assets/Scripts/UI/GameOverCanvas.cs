using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

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
        gameObject.SetActive(isVisible);
    }
}
