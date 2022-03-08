using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    PlayerInputActions playerInputActions;
    InputAction movementInput;
    Camera mainCamera;
    Movement movement;

    bool rightButtonPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        movement = GetComponent<Movement>();

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
        TryMoveToClickedPosition();
    }

    void HandleMousePressedDown(InputAction.CallbackContext ctx)
    {
        rightButtonPressed = true;
    }

    void HandleMouseReleased(InputAction.CallbackContext ctx)
    {
        rightButtonPressed = false;
    }

    void TryMoveToClickedPosition()
    {
        if (rightButtonPressed)
        {
            RaycastHit rayHit;
            Ray clickedPosition = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            bool hasClickedGround = Physics.Raycast(clickedPosition, out rayHit, Mathf.Infinity, groundLayer);

            if (hasClickedGround)
            {
                movement.MoveTo(rayHit.point);
            }
        }
    }
}
