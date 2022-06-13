using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

using RPG.Attributes;
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

        public static event Action onPlayerDeath;

        // Start is called before the first frame update
        void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            attacking = GetComponent<Attacking>();
            movement = GetComponent<UnitMovement>();
            mainCamera = Camera.main;
            health = GetComponent<Health>();

            playerInputActions = new PlayerInputActions();
            movementInput = playerInputActions.Player.Movement;
        }

        void OnEnable()
        {
            health.onUnitDeath += HandleDeath;

            movementInput.performed += HandleMousePressedDown;
            movementInput.canceled += HandleMouseReleased;
        }

        void Start()
        {
            movementInput.Enable();
        }

        void OnDisable()
        {
            health.onUnitDeath -= HandleDeath;

            movementInput.performed -= HandleMousePressedDown;
            movementInput.canceled -= HandleMouseReleased;

            movementInput.Disable();
        }

        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        void HandleDeath()
        {
            onPlayerDeath?.Invoke();
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
