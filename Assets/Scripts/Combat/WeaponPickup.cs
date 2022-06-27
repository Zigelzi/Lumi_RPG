using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon;
        [SerializeField][Range(0, 20)] float respawnTime = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                WeaponManager weaponManager = other.GetComponent<WeaponManager>();
                weaponManager.EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        public bool HandleRaycast(PlayerController player, RaycastHit hit)
        {
            player.TryStartMoveAction(hit.point);
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Interactable;
        }

        IEnumerator HideForSeconds(float duration)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(duration);
            ShowPickup(true);
        }

        void ShowPickup(bool isVisible)
        {
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(isVisible);
            }
            collider.enabled = isVisible;
        }

        
    }
}

