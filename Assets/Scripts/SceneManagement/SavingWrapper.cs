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

        InputAction savingInput;
        InputAction loadingInput;
        InputAction deleteSaveInput;
        PlayerInputActions playerInputActions;
        SavingSystem savingSystem;

        const string defaultSaveFile = "lumi_save";

        void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
            
            playerInputActions = new PlayerInputActions();
            
            savingInput = playerInputActions.Player.Save;
            loadingInput = playerInputActions.Player.Load;
            deleteSaveInput = playerInputActions.Player.DeleteSave;

            StartCoroutine(LoadLastScene());
        }

        void OnEnable()
        {
            savingInput.performed += Save;
            loadingInput.performed += Load;
            deleteSaveInput.performed += DeleteSave;
        }

        void Start()
        {
            savingInput.Enable();
            loadingInput.Enable();
            deleteSaveInput.Enable();

        }

        void OnDisable()
        {
            savingInput.Disable();
            loadingInput.Disable();

            savingInput.performed -= Save;
            loadingInput.performed -= Load;
            deleteSaveInput.performed -= DeleteSave;
        }

        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }

        public void DeleteSave()
        {
            savingSystem.Delete(defaultSaveFile);
        }

        void Save(InputAction.CallbackContext ctx)
        {
            savingSystem.Save(defaultSaveFile);
        }

        void Load(InputAction.CallbackContext ctx)
        {
            savingSystem.Load(defaultSaveFile);
        }

        
        void DeleteSave(InputAction.CallbackContext ctx)
        {
            savingSystem.Delete(defaultSaveFile);
        }

        IEnumerator LoadLastScene()
        {
            yield return savingSystem.LoadLastScene(defaultSaveFile);

            CanvasFader canvasFader = FindObjectOfType<CanvasFader>();
            canvasFader.SetCanvasToTransparent();
            yield return canvasFader.FadeToTransparent(loadingDuration);
        }
    }
}

