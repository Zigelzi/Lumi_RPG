using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    InputAction movementInput;

    NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

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
        Debug.Log("Clicked!");
    }
}
