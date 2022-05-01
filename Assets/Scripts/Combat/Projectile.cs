using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        
        Health currentTarget;

        // Start is called before the first frame update
        void Update()
        {
            FlyTowards();
        }

        public void SetTarget(Health target)
        {
            currentTarget = target;
        }

        void FlyTowards()
        {
            Vector3 aimLocation = GetAimLocation();
            transform.LookAt(aimLocation);
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = currentTarget.GetComponent<CapsuleCollider>();
            Vector3 targetCenter = Vector3.up * targetCollider.height / 2;
            return currentTarget.transform.position + targetCenter;
        }
    }
}

