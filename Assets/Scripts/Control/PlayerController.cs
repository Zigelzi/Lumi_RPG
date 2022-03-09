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
        [SerializeField] LayerMask clickableLayer;

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
            HandleMouseClick();
        }

        void HandleMousePressedDown(InputAction.CallbackContext ctx)
        {
            rightButtonPressed = true;
        }

        void HandleMouseReleased(InputAction.CallbackContext ctx)
        {
            rightButtonPressed = false;
        }

        void HandleMouseClick()
        {
            if (rightButtonPressed)
            {
                TryMoveAndAttack();
            }
        }

        void TryMoveAndAttack()
        {
            bool hasClickedValidPosition = Physics.Raycast(
                GetMouseRay(),
                out RaycastHit rayHit,
                Mathf.Infinity,
                clickableLayer
                );

            if (hasClickedValidPosition)
            {
                if (rayHit.collider.TryGetComponent<EnemyController>(out EnemyController target))
                {
                    attacking.Attack();
                    movement.MoveTo(target.transform.position);
                }
                else
                {
                    movement.MoveTo(rayHit.point);
                }
            }
        }

        private Ray GetMouseRay()
        {
            return mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}
