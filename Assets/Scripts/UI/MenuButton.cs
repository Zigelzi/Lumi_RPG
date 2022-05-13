using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void LoadLastSave()
    {
        Debug.Log("Clicked");
        SavingWrapper saving = FindObjectOfType<SavingWrapper>();
        saving.Load();
    }
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void Quit()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    
}
