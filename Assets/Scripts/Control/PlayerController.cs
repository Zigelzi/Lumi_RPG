using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

using RPG.Attributes;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;
using RPG.UI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] LayerMask interactableLayers;
        [SerializeField] Transform spellCastingPoint;

        ActionScheduler actionScheduler;
        Attacking attacking;
        Camera mainCamera;
        CursorManager cursor;
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
            cursor = GetComponent<CursorManager>();
            movement = GetComponent<UnitMovement>();
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
            mainCamera = Camera.main;
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

            cursor.SetCursor(CursorType.Unclickable);
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

                cursor.SetCursor(CursorType.Combat);
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
                cursor.SetCursor(CursorType.Movement);

                return true;
            }
            
            return false;
            
        }
    }
}
