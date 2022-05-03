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
        
        Health currentTarget;
        float damage = 0;

        public float Damage { set { damage = value; } }

        // Start is called before the first frame update
        void Update()
        {
            FlyTowards();
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Health>(out Health health))
            {
                if(IsEnemy(health))
                {
                    health.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
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

        bool IsEnemy(Health health)
        {
            if(health.TryGetComponent<EnemyController>(out EnemyController enemy))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

