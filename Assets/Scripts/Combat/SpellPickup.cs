using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class SpellPickup : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                ConsumePickup(other);
            }    
        }

        void ConsumePickup(Collider playerCollider)
        {
            Casting casting = playerCollider.gameObject.GetComponent<Casting>();

            if (casting != null && !casting.enabled)
            {
                casting.enabled = true;
                Destroy(gameObject);
            }
        } 
    }
}

