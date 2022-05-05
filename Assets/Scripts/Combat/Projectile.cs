using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Control;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float lifetime = 5f;
        
        Health currentTarget;
        float damage = 0;

        void Start()
        {
            Vector3 aimLocation = GetAimLocation();
            transform.LookAt(aimLocation);
            Invoke("DestroyProjectile", lifetime);
        }

        // Start is called before the first frame update
        void Update()
        {
            MoveForward();
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Health>(out Health collidedObject))
            {
                if(collidedObject == currentTarget)
                {
                    collidedObject.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }

        public void SetTarget(Health target)
        {
            currentTarget = target;
        }

        public void SetDamage(float newDamage)
        {
            damage = newDamage;
        }

        void MoveForward()
        {
            
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = currentTarget.GetComponent<CapsuleCollider>();
            Vector3 targetCenter = Vector3.up * targetCollider.height / 2;
            return currentTarget.transform.position + targetCenter;
        }

        void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
}

