using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
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

        IEnumerator HideForSeconds(float duration)
        {
            TogglePickup(false);
            yield return new WaitForSeconds(duration);
            TogglePickup(true);
        }

        void TogglePickup(bool isVisible)
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

