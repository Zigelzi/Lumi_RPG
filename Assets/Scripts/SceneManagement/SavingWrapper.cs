using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField][Range(0, 5f)] float loadingDuration = 1f;

        CanvasFader canvasFader;
        InputAction savingInput;
        InputAction loadingInput;
        PlayerInputActions playerInputActions;
        SavingSystem savingSystem;

        const string defaultSaveFile = "lumi_save";

        // Start is called before the first frame update
        IEnumerator Start()
        {
            playerInputActions = new PlayerInputActions();
            savingSystem = GetComponent<SavingSystem>();
            canvasFader = FindObjectOfType<CanvasFader>();

            savingInput = playerInputActions.Player.Save;
            loadingInput = playerInputActions.Player.Load;

            savingInput.Enable();
            loadingInput.Enable();

            savingInput.performed += Save;
            loadingInput.performed += Load;

            canvasFader.SetCanvasToOpaque();
            yield return savingSystem.LoadLastScene(defaultSaveFile);
            yield return canvasFader.FadeIn(loadingDuration);
            
        }

        void OnDestroy()
        {
            savingInput.Disable();
            loadingInput.Disable();

            savingInput.performed -= Save;
            loadingInput.performed -= Load;
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }

        void Save(InputAction.CallbackContext ctx)
        {
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }

        void Load(InputAction.CallbackContext ctx)
        {
            savingSystem.Load(defaultSaveFile);
        }
    }
}

