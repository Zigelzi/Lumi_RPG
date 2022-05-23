using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Attributes;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] TMP_Text healthValue;

    GameObject player;
    Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        SetHealthValue();

        playerHealth.OnHealthChange += HandleHealthUpdate;
    }

    void OnDestroy()
    {
        playerHealth.OnHealthChange -= HandleHealthUpdate;
    }

    void SetHealthValue()
    {
        if (healthValue == null) return;

        string healthText = $"{playerHealth.CurrentHealth} / {playerHealth.MaxHealth}";
        healthValue.text = healthText;
    }
    void SetHealthValue(float value)
    {
        if (healthValue == null) return;

        string healthText = $"{value} / {playerHealth.MaxHealth}";
        healthValue.text = healthText;
    }

    void HandleHealthUpdate(float newHealth)
    {
        SetHealthValue(newHealth);
    }
    
}
