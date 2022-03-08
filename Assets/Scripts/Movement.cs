using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    PlayerInputActions playerInputActions;
    InputAction movementInput;

    Animator animator;
    Camera mainCamera;
    NavMeshAgent navAgent;

    bool rightButtonPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();

        playerInputActions = new PlayerInputActions();

        movementInput = playerInputActions.Player.Movement;
        movementInput.Enable();

        movementInput.performed += HandleMousePressedDown;
        movementInput.canceled += HandleMouseReleased;
    }

    void OnDestroy() {
        movementInput.Disable();
        movementInput.performed -= HandleMousePressedDown;
        movementInput.canceled -= HandleMouseReleased;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
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
                navAgent.SetDestination(rayHit.point);
            }
        }
    }

    void UpdateAnimator()
    {
        Vector3 playerVelocity = navAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(playerVelocity);
        float forwardSpeed = localVelocity.z;

        // String reference defined in the character animator
        animator.SetFloat("forwardSpeed", forwardSpeed);

    }
}
