using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;
using RPG.Combat;

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

        public override void StopTargeting(GameObject user)
        {
            Casting casting = user.GetComponent<Casting>();
            casting.IsTargeting = false;
        }

        IEnumerator Targeting(GameObject user, PlayerController playerController)
        {
            CursorManager cursorManager = playerController.GetComponent<CursorManager>();
            Casting casting = user.GetComponent<Casting>();

            //playerController.IsInputAllowed = false;
            casting.IsTargeting = true;
            while (casting.IsTargeting)
            {
                if (cursorManager != null)
                {
                    cursorManager.SetCursor(CursorType.Targeting);
                }

                if (playerController.LeftButtonPressed)
                {
                    //playerController.IsInputAllowed = true;
                    casting.IsTargeting = false;
                    yield break;
                }
                // Run in the beginning of every frame
                yield return null;
            }
        }
    }
}
