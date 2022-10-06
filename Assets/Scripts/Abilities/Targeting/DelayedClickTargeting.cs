using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;
using RPG.Combat;
using System;
using UnityEngine.InputSystem;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Targeting_Delayed_Click_", menuName = "Abilities/Targeting/Delayed click targeting", order = 1)]
    public class DelayedClickTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask targetableLayers;
        public override void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> onTargetingFinished)
        {
            PlayerController playerController = user.GetComponent<PlayerController>();
            
            playerController.StartCoroutine(Targeting(user, playerController, onTargetingFinished));
        }

        public override void StopTargeting(GameObject user)
        {
            Casting casting = user.GetComponent<Casting>();
            casting.IsTargeting = false;
        }

        IEnumerator Targeting(GameObject user, 
            PlayerController playerController, 
            Action<IEnumerable<GameObject>> onTargetingFinished)
        {
            CursorManager cursorManager = playerController.GetComponent<CursorManager>();
            Casting casting = user.GetComponent<Casting>();

            casting.IsTargeting = true;
            while (casting.IsTargeting)
            {
                FaceTowardsCursor(user);
                if (cursorManager != null)
                {
                    cursorManager.SetCursor(CursorType.Targeting);
                }

                if (playerController.LeftButtonPressed)
                {
                    casting.IsTargeting = false;
                    onTargetingFinished(GetTargetInMousePosition());
                    yield break;
                }
                // Run in the beginning of every frame
                yield return null;
            }
        }

        void FaceTowardsCursor(GameObject user)
        {
            if (Physics.Raycast(PlayerController.GetMouseRay(),
                out RaycastHit rayhit,
                Mathf.Infinity))
            {
                user.transform.LookAt(rayhit.point);
            }
        }

        IEnumerable<GameObject> GetTargetInMousePosition()
        {
            if (Physics.Raycast(PlayerController.GetMouseRay(),
                out RaycastHit rayHit,
                Mathf.Infinity,
                targetableLayers))
            {
                GameObject target = rayHit.collider.gameObject;
                yield return target;
            }
        }
    }
}
