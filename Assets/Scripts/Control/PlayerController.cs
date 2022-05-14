using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

using RPG.Core;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] LayerMask interactableLayers;
        [SerializeField] Transform spellCastingPoint;

        ActionScheduler actionScheduler;
        Attacking attacking;
        Camera mainCamera;
        Health health;
        InputAction movementInput;
        PlayerInputActions playerInputActions;
        UnitMovement movement;

        bool rightButtonPressed = false;

        public PlayerInputActions PlayerInputActions { get { return playerInputActions; } }
        public Transform SpellCastingPoint { get { return spellCastingPoint; } }

        public static event Action OnPlayerDeath;

        // Start is called before the first frame update
        void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            mainCamera = Camera.main;
            
            movement = GetComponent<UnitMovement>();
            attacking = GetComponent<Attacking>();
            health = GetComponent<Health>();

            health.OnUnitDeath += HandleDeath;
            

            playerInputActions = new PlayerInputActions();

            movementInput = playerInputActions.Player.Movement;
            movementInput.Enable();

            movementInput.performed += HandleMousePressedDown;
            movementInput.canceled += HandleMouseReleased;
        }
        void OnDestroy()
        {
            health.OnUnitDeath -= HandleDeath;

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

        void HandleDeath()
        {
            OnPlayerDeath?.Invoke();
            enabled = false;
            actionScheduler.CancelCurrentAction();
            movement.SetNavAgent(false);
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

            bool isEnemy = rayHit.collider.TryGetComponent<CombatTarget>(
                out CombatTarget target
                );
            

            if (isHoveringOverInteractable &&
                isEnemy)
            {
                if (rightButtonPressed)
                {
                    attacking.StartAttackAction(target.gameObject);
                }
                return true;
            }

            return false;
        }

        Ray GetMouseRay()
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
                    movement.StartMovementAction(rayHit.point);
                }
                return true;
            }
            
            return false;
            
        }
    }
}
