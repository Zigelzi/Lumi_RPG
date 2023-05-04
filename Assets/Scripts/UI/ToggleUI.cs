using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.UI
{
    public class ToggleUI : MonoBehaviour
    {
        [SerializeField] GameObject _toggledElement;

        InputAction _toggleTraitInput;
        PlayerInputActions _playerInputActions;

        void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _toggleTraitInput = _playerInputActions.UI.ToggleTraits;
        }

        void OnEnable()
        {
            _toggleTraitInput.performed += HandleToggle;
        }

        void Start()
        {

            _toggleTraitInput.Enable();

            if (_toggledElement == null) return;

            _toggledElement.SetActive(false);
        }

        void OnDisable()
        {
            _toggleTraitInput.performed -= HandleToggle;
        }

        void HandleToggle(InputAction.CallbackContext ctx)
        {
            _toggledElement.SetActive(!_toggledElement.activeSelf);
        }

        public void Toggle()
        {
            _toggledElement.SetActive(!_toggledElement.activeSelf);
        }

    }

}