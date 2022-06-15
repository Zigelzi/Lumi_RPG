using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameDevTV.Utils;

using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float currentExperience;
        [SerializeField] LazyValue<float> requiredExperience;

        BaseStats playerStats;

        public float CurrentExperience { get { return currentExperience; } }
        public float RequiredExperience {  get { return requiredExperience.value; } }

        public event Action<float, float> onExperienceChange;

        void Awake()
        {
            playerStats = GetComponent<BaseStats>();
            requiredExperience = new LazyValue<float>(GetStartingExperienceRequirement);
        }

        void Start()
        {
            requiredExperience.ForceInit();
        }

        public void AddExperience(float amount)
        {
            float experienceAmount = Mathf.Min(requiredExperience.value - currentExperience, amount);
            currentExperience += experienceAmount;

            if (currentExperience == requiredExperience.value)
            {
                playerStats.LevelUp();
                currentExperience = 0;
                requiredExperience.value = playerStats.GetStat(Stat.ExperienceRequirement);
            }
            onExperienceChange?.Invoke(currentExperience, requiredExperience.value);
        }

        public object CaptureState()
        {
            return currentExperience;
        }

        public void RestoreState(object state)
        {
            float restoredExperience = (float)state;

            currentExperience = restoredExperience;
            onExperienceChange?.Invoke(currentExperience, requiredExperience.value);

        }

        float GetStartingExperienceRequirement()
        {
            return playerStats.GetStat(Stat.ExperienceRequirement);
        }
    }
}

