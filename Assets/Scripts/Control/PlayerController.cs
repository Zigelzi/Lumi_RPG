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
        [SerializeField] float maxPathLength = 20f;
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
            RaycastHit[] hits = GetRaycastHitsByDistance();

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
            
            if (isHoveringOverNavMesh && IsValidPathAvailable(targetPosition))
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

        bool IsValidPathAvailable(Vector3 targetPosition)
        {
            NavMeshPath path = new NavMeshPath();

            bool isPathFound = NavMesh.CalculatePath(
                transform.position,
                targetPosition,
                NavMesh.AllAreas,
                path);

            float pathLength = GetPathLength(path);

            if (isPathFound 
                && pathLength <= maxPathLength
                && path.status == NavMeshPathStatus.PathComplete)
            {
                return true;
            }

            return false;
        }
        
        float GetPathLength(NavMeshPath path)
        {
            float totalPathLength = 0;

            if (path.corners.Length < 2) return totalPathLength;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                totalPathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return totalPathLength;
        }
    }
}
