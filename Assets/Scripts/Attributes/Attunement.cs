using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using RPG.Saving;
using System;
using UnityEngine.AI;

namespace RPG.Attributes
{
    public class Attunement : MonoBehaviour, ISaveable
    {
        [SerializeField] float currentAttunement = 0f;
        [SerializeField] float maxAttunement = 100f;
        [SerializeField] float regenSpeed = 1f;

        public float CurrentAttunement { get { return currentAttunement; } }
        public float MaxAttunement { get { return maxAttunement; } }

        public AttunementChangeEvent onAttunementChange;

        [Serializable]
        public class AttunementChangeEvent : UnityEvent<float> {}

        void Awake()
        {
            currentAttunement = maxAttunement;    
        }

        void Start()
        {
            onAttunementChange?.Invoke(currentAttunement);
            StartCoroutine(RegenAttunement());
        }

        void Update()
        {
            IsAbleToRegen();
        }

        public bool HasRequiredAttunement(float amount)
        {
            if (currentAttunement >= amount)
            {
                return true;
            }

            return false;
        }

        public void ConsumeAttunement(float amount)
        {
            currentAttunement = Mathf.Max(currentAttunement - amount, 0);
            onAttunementChange?.Invoke(currentAttunement);
        }

        public object CaptureState()
        {
            return currentAttunement;
        }

        public void RestoreState(object state)
        {
            float restoredAttunement = (float)state;
            currentAttunement = restoredAttunement;
            onAttunementChange?.Invoke(currentAttunement);
        }

        IEnumerator RegenAttunement()
        {
            while (true)
            {
                if (currentAttunement < maxAttunement && IsAbleToRegen())
                {
                    yield return new WaitForSeconds(1f);
                    float regenAmount = Mathf.Min(maxAttunement - currentAttunement, regenSpeed);
                    currentAttunement += regenAmount;
                    onAttunementChange?.Invoke(currentAttunement);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        bool IsAbleToRegen()
        {
            float playerVelocity = transform.GetComponent<NavMeshAgent>().velocity.magnitude;
            if (Mathf.Approximately(playerVelocity, 0)) {
                return true;
            }

            return false;
        }
    }
}
