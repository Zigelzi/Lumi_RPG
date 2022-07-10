using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Attributes;

public class HealthBar : MonoBehaviour
{
    Health health;
    Slider healthSlider;

    void Awake()
    {
        health = GetComponentInParent<Health>();
        healthSlider = GetComponentInChildren<Slider>();    
    }

    void OnEnable()
    {
        health.onHealthChange += HandleHealthUpdate;
        health.onUnitDeath += HandleUnitDeath;
    }

    void Start()
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = health.CurrentHealth;
    }

    void OnDisable()
    {
        health.onHealthChange -= HandleHealthUpdate;
        health.onUnitDeath -= HandleUnitDeath;
    }

    void HandleHealthUpdate(float newHealth)
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = newHealth;
    }

    void HandleUnitDeath()
    {
        gameObject.SetActive(false);
    }
}
