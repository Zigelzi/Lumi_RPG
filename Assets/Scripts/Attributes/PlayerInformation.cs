using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Stats;

namespace RPG.Attributes
{
    public class PlayerInformation : MonoBehaviour
    {
        [SerializeField] TMP_Text healthValue;
        [SerializeField] TMP_Text levelValue;
        [SerializeField] TMP_Text experienceValue;

        GameObject player;
        BaseStats playerStats;
        Experience playerExperience;
        Health playerHealth;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            playerStats = player.GetComponent<BaseStats>();
            playerExperience = player.GetComponent<Experience>();

            SetHealthValue();
            SetStartingLevel();
            SetExperience();

            playerHealth.OnHealthChange += HandleHealthUpdate;
            playerExperience.OnExperienceChange += HandleExperienceUpdate;

        }

        void OnDestroy()
        {
            playerHealth.OnHealthChange -= HandleHealthUpdate;
            playerExperience.OnExperienceChange -= HandleExperienceUpdate;
        }

        void HandleHealthUpdate(float newHealth)
        {
            SetHealthValue(newHealth);
        }

        void HandleExperienceUpdate(float newExperience)
        {
            SetExperience(newExperience);
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

        void SetExperience()
        {
            if (experienceValue == null || playerExperience == null) return;

            experienceValue.text = $"{playerExperience.CurrentExperience} / {playerExperience.RequiredExperience}";
        }

        void SetExperience(float amount)
        {
            if (experienceValue == null || playerExperience == null) return;

            experienceValue.text = $"{amount} / {playerExperience.RequiredExperience}";
        }
    }
}

