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
        [SerializeField] GameObject regenVfxPrefab;

        BaseStats baseStats;
        GameObject regenVfxInstance = null;
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
            regenVfxInstance = null;
        }

        void OnEnable()
        {
            baseStats.onLevelChange += HandleLevelChange;
        }

        void Start()
        {
            SetStartingAttunement();
        }

        void Update()
        {
            RegenAttunement();
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

        void RegenAttunement()
        {
            ManageRegenVFX();
            if (IsAbleToRegen())
            {
                float regenAmount = Mathf.Min(maxAttunement.value - currentAttunement, regenRate * Time.deltaTime);
                currentAttunement += regenAmount;
                onAttunementChange?.Invoke(currentAttunement);
            }
            
        }

        bool IsAbleToRegen()
        {
            float playerVelocity = transform.GetComponent<NavMeshAgent>().velocity.magnitude;
            if (Mathf.Approximately(playerVelocity, 0) && currentAttunement < maxAttunement.value) {
                return true;
            }

            return false;
        }

        void ManageRegenVFX()
        {
            if (regenVfxPrefab == null) return;

            if (IsAbleToRegen())
            {
                SpawnRegenFX();
            }
            else
            {
                StopRegenFX();
            }
            
        }

        void SpawnRegenFX()
        {
            if (regenVfxInstance == null)
            {
                regenVfxInstance = Instantiate(regenVfxPrefab, transform.position, transform.rotation, transform);
            }
        }

        void StopRegenFX()
        {
            if (regenVfxInstance == null) return;

            ParticleSystem[] particleSystems = regenVfxInstance.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Stop();
            }
        }
    }
}
