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
    }

    void Start()
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = health.MaxHealth;
    }

    void OnDisable()
    {
        health.onHealthChange -= HandleHealthUpdate;
    }

    void HandleHealthUpdate(float newHealth)
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = newHealth;
    }
}
