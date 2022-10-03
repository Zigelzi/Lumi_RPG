using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;
using RPG.Combat;
using System;
using UnityEngine.InputSystem;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Delayed click targeting", menuName = "Abilities/Targeting/Delayed click targeting", order = 0)]
    public class DelayedClickTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask targetableLayers;
        public override void StartTargeting(GameObject user, Action<IEnumerable<GameObject>> targetingFinished)
        {
            PlayerController playerController = user.GetComponent<PlayerController>();
            
            playerController.StartCoroutine(Targeting(user, playerController, targetingFinished));
        }

        public override void StopTargeting(GameObject user)
        {
            Casting casting = user.GetComponent<Casting>();
            casting.IsTargeting = false;
        }

        IEnumerator Targeting(GameObject user, 
            PlayerController playerController, 
            Action<IEnumerable<GameObject>> targetingFinished)
        {
            CursorManager cursorManager = playerController.GetComponent<CursorManager>();
            Casting casting = user.GetComponent<Casting>();

            casting.IsTargeting = true;
            while (casting.IsTargeting)
            {
                if (cursorManager != null)
                {
                    cursorManager.SetCursor(CursorType.Targeting);
                }

                if (playerController.LeftButtonPressed)
                {
                    casting.IsTargeting = false;
                    targetingFinished(GetTargetInMousePosition());
                    yield break;
                }
                // Run in the beginning of every frame
                yield return null;
            }
        }

        IEnumerable<GameObject> GetTargetInMousePosition()
        {
            if (Physics.Raycast(PlayerController.GetMouseRay(),
                out RaycastHit rayHit,
                Mathf.Infinity,
                targetableLayers))
            {
                CombatTarget target = rayHit.collider.GetComponent<CombatTarget>();
                yield return target.gameObject;
            }
        }
    }
}
