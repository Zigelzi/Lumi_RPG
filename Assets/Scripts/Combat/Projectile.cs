﻿using System.Collections;
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
            Invoke(nameof(DestroyProjectile), lifetime);

            if (currentTarget == null) return;

            Vector3 aimLocation = GetAimLocation();
            transform.LookAt(aimLocation);
        }

        void Update()
        {
            MoveForward();
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Health>(out Health collidedObject))
            {                
                if (isHoming)
                {
                    // Homing projectiles only collide with their target
                    CollideWithCurrentTarget(collidedObject);
                }
                else
                {
                    // Enemy projectiles only collide with player
                    // Player projectiles collide with all enemies
                    CollideWithHostileTarget(collidedObject);
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

        void CollideWithCurrentTarget(Health collidedObject) 
        {
            if (collidedObject == currentTarget && collidedObject.IsAlive)
            {
                Collide(collidedObject);
            }
        }

        void CollideWithHostileTarget(Health collidedObject)
        {
            if (owner.gameObject.CompareTag("Player") && collidedObject.CompareTag("Enemy"))
            {
                Collide(collidedObject);
            }
            else if (owner.gameObject.CompareTag("Enemy") && collidedObject.CompareTag("Player"))
            {
                Collide(collidedObject);
            }
        }

        void Collide(Health collidedObject)
        {
            if (owner == collidedObject.gameObject) return;

            collidedObject.TakeDamage(damage, owner);
            PlayHitFX(collidedObject.GetComponent<CapsuleCollider>());
            onProjectileHit?.Invoke();
            speed = 0;

            DestroyOnHitObjects();
        }

        void PlayHitFX(CapsuleCollider collider)
        {
            if (hitEffect == null) return;
            
            Vector3 hitPosition = collider.ClosestPoint(transform.position);
            Instantiate(hitEffect, hitPosition, transform.rotation);
        }
    }
}

