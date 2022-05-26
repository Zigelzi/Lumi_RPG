using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Stats;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float currentExperience = 0;
        [SerializeField] float requiredExperience = 0;

        BaseStats playerStats;

        public float CurrentExperience { get { return currentExperience; } }
        public float RequiredExperience {  get { return requiredExperience; } }

        public Action<float> OnExperienceChange;

        void Awake()
        {
            playerStats = GetComponent<BaseStats>();
            requiredExperience = playerStats.GetStartingStat(Stat.ExperienceRequirement);
        }

        public void AddExperience(float amount)
        {
            currentExperience += amount;
            OnExperienceChange?.Invoke(currentExperience);
        }
    }
}

