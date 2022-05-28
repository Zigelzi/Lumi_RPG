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

        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = health.MaxHealth;

        health.OnHealthChange += HandleHealthUpdate;
    }

    void OnDestroy()
    {
        health.OnHealthChange -= HandleHealthUpdate;
    }

    void HandleHealthUpdate(float newHealth)
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = newHealth;
    }
}
