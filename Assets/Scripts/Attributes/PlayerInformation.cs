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

        GameObject player;
        BaseStats playerStats;
        Health playerHealth;

        // Start is called before the first frame update
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            playerStats = player.GetComponent<BaseStats>();

            SetHealthValue();
            SetStartingLevel();

            playerHealth.OnHealthChange += HandleHealthUpdate;
        }

        void OnDestroy()
        {
            playerHealth.OnHealthChange -= HandleHealthUpdate;
        }

        void HandleHealthUpdate(float newHealth)
        {
            SetHealthValue(newHealth);
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
    }
}

