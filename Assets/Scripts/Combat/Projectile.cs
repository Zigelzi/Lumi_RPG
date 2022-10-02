using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float lifetime = 5f;
        [SerializeField] float lifetimeAfterImpact = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect;
        [SerializeField] GameObject[] onHitDestroyedObjects = null;

        GameObject owner;
        Health currentTarget;
        float damage = 0;

        public UnityEvent onProjectileHit;

        void Start()
        {
            if (isHoming)
            {
                Vector3 aimLocation = GetAimLocation();
                transform.LookAt(aimLocation);
            }
            Invoke(nameof(DestroyProjectile), lifetime);
        }

        void Update()
        {
            MoveForward();
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Health>(out Health collidedObject))
            {
                if(collidedObject == currentTarget && collidedObject.IsAlive)
                {
                    collidedObject.TakeDamage(damage, owner);
                    PlayHitFX();
                    onProjectileHit?.Invoke();
                    speed = 0;

                    DestroyOnHitObjects();
                    
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

        public void SetProjectileOwner(GameObject owner)
        {
            this.owner = owner;
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = currentTarget.GetComponent<CapsuleCollider>();
            Vector3 targetCenter = Vector3.up * targetCollider.height / 2;
            return currentTarget.transform.position + targetCenter;
        }

        void DestroyOnHitObjects()
        {
            if (onHitDestroyedObjects.Length > 0)
            {
                foreach (GameObject destroyable in onHitDestroyedObjects)
                {
                    Destroy(destroyable);
                }
                Invoke(nameof(DestroyProjectile), lifetimeAfterImpact);
            }
            else
            {
                DestroyProjectile();
            }
            
        }

        void DestroyProjectile()
        {
            Destroy(gameObject);
        }

        void MoveForward()
        {
            if (isHoming && currentTarget.IsAlive)
            {
                Vector3 aimLocation = GetAimLocation();
                transform.LookAt(aimLocation);
            }
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        void PlayHitFX()
        {
            if (hitEffect == null) return;

            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }
    }
}

