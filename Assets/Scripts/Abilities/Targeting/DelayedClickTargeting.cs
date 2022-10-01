using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Delayed click targeting", menuName = "Abilities/Targeting/Delayed click targeting", order = 0)]
    public class DelayedClickTargeting : TargetingStrategy
    {
        
        public override void StartTargeting(GameObject user)
        {
            PlayerController playerController = user.GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(user, playerController));
        }

        IEnumerator Targeting(GameObject user, PlayerController playerController)
        {
            CursorManager cursorManager = playerController.GetComponent<CursorManager>();
            playerController.IsInputAllowed = false;
            while (true)
            {
                if (cursorManager != null)
                {
                    cursorManager.SetCursor(CursorType.Targeting);
                }

                if (playerController.LeftButtonPressed)
                {
                    break;
                }
                // Run in the beginning of every frame
                yield return null;
            }

            playerController.IsInputAllowed = true;
        }
    }
}
