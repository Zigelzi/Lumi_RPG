using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

using GameDevTV.Utils;

using RPG.Saving;
using RPG.Stats;

namespace RPG.Attributes
{
    public class Attunement : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenRate = 1f;
        [SerializeField] float regenSpeed = 1f;

        BaseStats baseStats;
        LazyValue<float> maxAttunement;
        float currentAttunement = -1f;

        public float CurrentAttunement { get { return currentAttunement; } }
        public float MaxAttunement { get { return maxAttunement.value; } }

        public AttunementChangeEvent onAttunementChange;

        [Serializable]
        public class AttunementChangeEvent : UnityEvent<float> {}

        void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            maxAttunement = new LazyValue<float>(GetStartingAttunement);
        }

        void OnEnable()
        {
            baseStats.onLevelChange += HandleLevelChange;
        }

        void Start()
        {
            SetStartingAttunement();
            StartCoroutine(RegenAttunement());
        }

        void Update()
        {
            IsAbleToRegen();
        }

        void OnDisable()
        {
            baseStats.onLevelChange -= HandleLevelChange;
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

        void HandleLevelChange(int newLevel)
        {
            maxAttunement.value = baseStats.GetStat(Stat.Attunement);
            currentAttunement = maxAttunement.value;

            onAttunementChange?.Invoke(currentAttunement);
        }

        float GetStartingAttunement()
        {
            return baseStats.GetStat(Stat.Attunement);
        }

        void SetStartingAttunement()
        {
            maxAttunement.ForceInit();

            if (currentAttunement < 0)
            {
                currentAttunement = maxAttunement.value;
            }

            onAttunementChange?.Invoke(currentAttunement);
        }

        IEnumerator RegenAttunement()
        {
            while (true)
            {
                if (currentAttunement < maxAttunement.value && IsAbleToRegen())
                {
                    yield return new WaitForSeconds(regenSpeed);
                    float regenAmount = Mathf.Min(maxAttunement.value - currentAttunement, regenRate);
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
