using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        InputAction savingInput;
        InputAction loadingInput;
        PlayerInputActions playerInputActions;
        SavingSystem savingSystem;

        const string defaultSaveFile = "lumi_save";

        // Start is called before the first frame update
        void Start()
        {
            playerInputActions = new PlayerInputActions();
            savingSystem = GetComponent<SavingSystem>();

            savingInput = playerInputActions.Player.Save;
            loadingInput = playerInputActions.Player.Load;

            savingInput.Enable();
            loadingInput.Enable();

            savingInput.performed += Save;
            loadingInput.performed += Load;
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

