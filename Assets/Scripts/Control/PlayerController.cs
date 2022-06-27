using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
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
        CursorManager cursor;
        Health health;
        InputAction movementInput;
        PlayerInputActions playerInputActions;
        UnitMovement movement;

        bool rightButtonPressed = false;

        public PlayerInputActions PlayerInputActions { get { return playerInputActions; } }
        public Transform SpellCastingPoint { get { return spellCastingPoint; } }

        public static event Action onPlayerDeath;

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
            if (InteractWithUI()) return;

            if (InteractWithComponent()) return;

            if (InteractWithMovement()) return;

            cursor.SetCursor(CursorType.Unclickable);
        }

        public void TryStartMoveAction(Vector3 position)
        {
            if (rightButtonPressed)
            {
                movement.StartMovementAction(position);
            }
        }

        public void TryStartAttackAction(GameObject target)
        {
            if (rightButtonPressed)
            {
                attacking.StartAttackAction(target);
            }
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

        bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                cursor.SetCursor(CursorType.UI);
                return true;
            }

            return false;
        }

        bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] components = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable component in components)
                {
                    if (component.HandleRaycast(this, hit))
                    {
                        cursor.SetCursor(component.GetCursorType());
                        return true;
                    }
                }
            }

            return false;
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
                TryStartMoveAction(rayHit.point);
                cursor.SetCursor(CursorType.Movement);

                return true;
            }
            
            return false;
            
        }

        Ray GetMouseRay()
        {
            return mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        
    }
}
