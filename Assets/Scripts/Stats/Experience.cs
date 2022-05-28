using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float currentExperience = 0;
        [SerializeField] float requiredExperience = 0;

        BaseStats playerStats;

        public float CurrentExperience { get { return currentExperience; } }
        public float RequiredExperience {  get { return requiredExperience; } }

        public Action<float, float> OnExperienceChange;

        void Awake()
        {
            playerStats = GetComponent<BaseStats>();
            requiredExperience = playerStats.GetStartingStat(Stat.ExperienceRequirement);
        }

        public void AddExperience(float amount)
        {
            float experienceAmount = Mathf.Min(requiredExperience - currentExperience, amount);
            currentExperience += experienceAmount;

            if (currentExperience == requiredExperience)
            {
                playerStats.LevelUp();
                currentExperience = 0;
                requiredExperience = playerStats.GetStat(Stat.ExperienceRequirement, playerStats.CurrentLevel);
            }
            OnExperienceChange?.Invoke(currentExperience, requiredExperience);
        }

        public object CaptureState()
        {
            return currentExperience;
        }

        public void RestoreState(object state)
        {
            float restoredExperience = (float)state;

            currentExperience = restoredExperience;
            OnExperienceChange?.Invoke(currentExperience, requiredExperience);

        }
    }
}

