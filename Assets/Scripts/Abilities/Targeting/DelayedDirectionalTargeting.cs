using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;
using RPG.Combat;
using System;
using UnityEngine.InputSystem;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Targeting_Delayed_Directional_", menuName = "Abilities/Targeting/Delayed directional targeting", order = 1)]
    public class DelayedDirectionalTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask targetableLayers;
        public override void StartTargeting(AbilityData data, Action onTargetingFinished)
        {
            PlayerController playerController = data.GetUser().GetComponent<PlayerController>();

            playerController.StartCoroutine(Targeting(data, onTargetingFinished));
        }

        public override void StopTargeting(GameObject user)
        {
            Casting casting = user.GetComponent<Casting>();
            casting.IsTargeting = false;
        }

        IEnumerator Targeting(AbilityData data,  
            Action onTargetingFinished)
        {
            GameObject user = data.GetUser();
            PlayerController playerController = user.GetComponent<PlayerController>();
            Casting casting = user.GetComponent<Casting>();

            casting.IsTargeting = true;
            while (casting.IsTargeting)
            {
                FaceTowardsCursor(user);
                SetCursorType(data);

                if (playerController.LeftButtonPressed)
                {
                    casting.IsTargeting = false;
                    data.SetTargets(GetTargetInMousePosition());
                    onTargetingFinished();
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
                Quaternion lookRotation = Quaternion.LookRotation(rayhit.point - user.transform.position);
                
                // Lock the rotation in X and Z axises
                lookRotation.x = 0;
                lookRotation.z = 0;
                
                user.transform.rotation = lookRotation;
            }
        }

        void SetCursorType(AbilityData data)
        {
            CursorManager cursorManager = data.GetUser().GetComponent<CursorManager>();
            if (cursorManager != null)
            {
                if (IsInCastRange(data))
                {
                    cursorManager.SetCursor(CursorType.Targeting);
                }
                cursorManager.SetCursor(CursorType.Unclickable);
            }

        }

        bool IsInCastRange(AbilityData data)
        {
            GameObject user = data.GetUser();
            if (Physics.Raycast(PlayerController.GetMouseRay(),
                out RaycastHit rayHit,
                Mathf.Infinity,
                targetableLayers))
            {

                if (Vector3.Distance(rayHit.point, user.transform.position) <= data.GetCastRange())
                {
                    return true;
                }
            }

            return false;
        }

        List<GameObject> GetTargetInMousePosition()
        {
            List<GameObject> targets = new List<GameObject>();
            if (Physics.Raycast(PlayerController.GetMouseRay(),
                out RaycastHit rayHit,
                Mathf.Infinity,
                targetableLayers))
            {
                targets.Add(rayHit.collider.gameObject);
            }
            return targets;
        }
    }
}
