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

    Camera mainCamera;

    NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

        playerInputActions = new PlayerInputActions();

        movementInput = playerInputActions.Player.Movement;
        movementInput.Enable();

        movementInput.performed += HandleMovementInput;
    }

    void OnDestroy() {
        movementInput.Disable();
        movementInput.performed -= HandleMovementInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleMovementInput(InputAction.CallbackContext ctx) {
        TryMoveToClickedPosition();
    }

    void TryMoveToClickedPosition()
    {
        RaycastHit rayHit;
        Ray clickedPosition = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        bool hasClickedGround = Physics.Raycast(clickedPosition, out rayHit, Mathf.Infinity, groundLayer);
        
        if (hasClickedGround) {
            navAgent.SetDestination(rayHit.point);
        }
    }
}
