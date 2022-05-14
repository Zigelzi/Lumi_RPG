using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    public void LoadLastSave()
    {
        SavingWrapper saving = FindObjectOfType<SavingWrapper>();
        saving.Load();
        HideGameOverCanvas();
    }
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        HideGameOverCanvas();
    }
    public void Quit()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    void HideGameOverCanvas()
    {
        gameOverCanvas.gameObject.SetActive(false);
    }
}
