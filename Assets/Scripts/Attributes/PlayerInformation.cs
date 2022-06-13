using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using RPG.Stats;

namespace RPG.Attributes
{
    public class PlayerInformation : MonoBehaviour
    {
        [SerializeField] TMP_Text healthValue;
        [SerializeField] TMP_Text levelValue;
        [SerializeField] TMP_Text experienceValue;
        [SerializeField] Slider experienceSlider;

        GameObject player;
        BaseStats playerStats;
        Experience playerExperience;
        Health playerHealth;

        // Start is called before the first frame update
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            playerStats = player.GetComponent<BaseStats>();
            playerExperience = player.GetComponent<Experience>();
        }

        void OnEnable()
        {
            playerHealth.onHealthChange += HandleHealthUpdate;
            playerExperience.onExperienceChange += HandleExperienceUpdate;
            playerStats.onLevelChange += HandleLevelUpdate;
        }

        void Start()
        {
            SetHealthValue();
            SetStartingLevel();
            SetExperience();
        }

        void OnDisable()
        {
            playerHealth.onHealthChange -= HandleHealthUpdate;
            playerExperience.onExperienceChange -= HandleExperienceUpdate;
            playerStats.onLevelChange -= HandleLevelUpdate;
        }

        void HandleHealthUpdate(float newHealth)
        {
            SetHealthValue(newHealth);
        }

        void HandleExperienceUpdate(float newCurrentExperience, float newRequiredExperience)
        {
            SetExperience(newCurrentExperience, newRequiredExperience);
        }

        void HandleLevelUpdate(int newLevel)
        {
            SetLevel(newLevel);
        }

        void SetHealthValue()
        {
            if (healthValue == null || playerHealth == null) return;

            string healthText = $"{playerHealth.CurrentHealth} / {playerHealth.MaxHealth}";
            healthValue.text = healthText;
        }

        void SetHealthValue(float value)
        {
            if (healthValue == null || playerHealth == null) return;

            string healthText = $"{value} / {playerHealth.MaxHealth}";
            healthValue.text = healthText;
        }

        void SetStartingLevel()
        {
            if (levelValue == null || playerStats == null) return;

            levelValue.text = playerStats.StartingLevel.ToString();
        }

        void SetLevel(int level)
        {
            if (levelValue == null || playerStats == null) return;

            levelValue.text = level.ToString();
        }

        void SetExperience()
        {
            if (experienceValue == null || 
                playerExperience == null || 
                experienceSlider == null) return;

            experienceValue.text = $"{playerExperience.CurrentExperience} / {playerExperience.RequiredExperience}";
            experienceSlider.maxValue = playerExperience.RequiredExperience;
            experienceSlider.value = playerExperience.CurrentExperience;
        }

        void SetExperience(float currentExperience, float requiredExperience)
        {
            if (experienceValue == null ||
                playerExperience == null ||
                experienceSlider == null) return;

            experienceValue.text = $"{currentExperience} / {requiredExperience}";

            experienceSlider.value = currentExperience;
            experienceSlider.maxValue = requiredExperience;
        }
    }
}

