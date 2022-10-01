using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System;

using RPG.Attributes;
using RPG.Core;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float navMeshProximityRange = 2f;
        [SerializeField] LayerMask interactableLayers;

        ActionScheduler actionScheduler;
        Attacking attacking;
        Camera mainCamera;
        CursorManager cursor;
        Health health;
        InputAction movementInput;
        InputAction selectInput;
        InputAction useInput;
        PlayerInputActions playerInputActions;
        UnitMovement movement;

        bool rightButtonPressed = false;
        bool leftButtonPressed = false;
        bool isInputAllowed = true;

        public PlayerInputActions PlayerInputActions { get { return playerInputActions; } }

        public bool LeftButtonPressed { get { return leftButtonPressed; } }
        public bool IsInputAllowed { get { return isInputAllowed; } set { isInputAllowed = value; } }

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
            selectInput = playerInputActions.Player.Select;
            useInput = playerInputActions.Player.Use;
        }

        void OnEnable()
        {
            health.onUnitDeath.AddListener(HandleDeath);

            movementInput.performed += HandleRightMouseButtonPressedDown;
            movementInput.canceled += HandleRightMouseButtonReleased;

            selectInput.performed += HandleLeftMouseButtonPressedDown;
            selectInput.canceled += HandleLeftMouseButtonReleased;


        }

        void Start()
        {
            movementInput.Enable();
            selectInput.Enable();

            mainCamera = Camera.main;
        }

        void OnDisable()
        {
            health.onUnitDeath.RemoveListener(HandleDeath);

            movementInput.performed -= HandleRightMouseButtonPressedDown;
            movementInput.canceled -= HandleRightMouseButtonReleased;

            selectInput.performed -= HandleLeftMouseButtonPressedDown;
            selectInput.canceled -= HandleLeftMouseButtonReleased;

            movementInput.Disable();
            selectInput.Disable();
        }

        void Update()
        {
            if (!IsInputAllowed) return;

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

        void HandleLeftMouseButtonPressedDown(InputAction.CallbackContext ctx)
        {
            leftButtonPressed = true;
            Debug.Log($"Leftbutton: {leftButtonPressed}");
        }

        void HandleLeftMouseButtonReleased(InputAction.CallbackContext ctx)
        {
            leftButtonPressed = false;
            Debug.Log($"Leftbutton: {leftButtonPressed}");
        }

        void HandleRightMouseButtonPressedDown(InputAction.CallbackContext ctx)
        {
            rightButtonPressed = true;
        }

        void HandleRightMouseButtonReleased(InputAction.CallbackContext ctx)
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
            RaycastHit[] hits = GetRaycastHitsByDistance();

            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] components = hit.transform.GetComponents<IRaycastable>();

                if (!movement.CanMoveTo(hit.transform.position)) return false;

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

        RaycastHit[] GetRaycastHitsByDistance()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            float[] distancesFromHit = new float[hits.Length];

            for(int i = 0; i < hits.Length; i++)
            {
                distancesFromHit[i] = hits[i].distance;
            }

            RaycastHit[] sortedHits = hits;
            Array.Sort(distancesFromHit, sortedHits);

            return sortedHits;

        }

        bool InteractWithMovement()
        {
            bool isHoveringOverNavMesh = RaycastNavMesh(out Vector3 targetPosition);
            
            if (isHoveringOverNavMesh && movement.CanMoveTo(targetPosition))
            {
                TryStartMoveAction(targetPosition);
                cursor.SetCursor(CursorType.Movement);           

                return true;
            }
            
            return false;
            
        }

        bool RaycastNavMesh(out Vector3 targetPosition)
        {
            bool isHoveringOverInteractable = Physics.Raycast(
                GetMouseRay(),
                out RaycastHit rayHit,
                Mathf.Infinity,
                interactableLayers
                );

            targetPosition = new Vector3();

            if (!isHoveringOverInteractable) return false;

            bool isNearNavMesh = NavMesh.SamplePosition(rayHit.point,
                    out NavMeshHit navHit,
                    navMeshProximityRange,
                    NavMesh.AllAreas);

            targetPosition = navHit.position;

            return isNearNavMesh;
        }

        Ray GetMouseRay()
        {
            return mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        
        
        
    }
}
