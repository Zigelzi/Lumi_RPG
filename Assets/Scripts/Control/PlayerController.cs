using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] LayerMask interactableLayers;

        PlayerInputActions playerInputActions;
        InputAction movementInput;
        Camera mainCamera;
        UnitMovement movement;
        Attacking attacking;

        bool rightButtonPressed = false;
        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
            movement = GetComponent<UnitMovement>();
            attacking = GetComponent<Attacking>();

            playerInputActions = new PlayerInputActions();

            movementInput = playerInputActions.Player.Movement;
            movementInput.Enable();

            movementInput.performed += HandleMousePressedDown;
            movementInput.canceled += HandleMouseReleased;
        }

        private void OnDestroy()
        {
            movementInput.Disable();
            movementInput.performed -= HandleMousePressedDown;
            movementInput.canceled -= HandleMouseReleased;
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        void HandleMousePressedDown(InputAction.CallbackContext ctx)
        {
            rightButtonPressed = true;
        }

        void HandleMouseReleased(InputAction.CallbackContext ctx)
        {
            rightButtonPressed = false;
        }

        bool InteractWithCombat()
        {
            bool isHoveringOverInteractable = Physics.Raycast(
                GetMouseRay(),
                out RaycastHit rayHit,
                Mathf.Infinity,
                interactableLayers
                );

            if (rayHit.collider == null) { return false; }

            bool isEnemy = rayHit.collider.TryGetComponent<EnemyController>(out EnemyController target);
            if (isHoveringOverInteractable && isEnemy)
            {
                if (rightButtonPressed)
                {
                    attacking.Attack(target);
                    //movement.MoveTo(target.transform.position);
                }
                return true;
            }

            return false;
        }

        private Ray GetMouseRay()
        {
            return mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        bool InteractWithMovement()
        {
            bool isHoveringOverInteractable = Physics.Raycast(
                GetMouseRay(),
                out RaycastHit rayHit,
                Mathf.Infinity,
                interactableLayers
                );

            if (isHoveringOverInteractable)
            {
                if (rightButtonPressed)
                {
                    movement.MoveTo(rayHit.point);
                }
                return true;
            }
            
            return false;
            
        }
    }
}
