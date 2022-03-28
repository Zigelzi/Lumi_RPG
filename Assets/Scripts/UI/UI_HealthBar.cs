using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core;

public class UI_HealthBar : MonoBehaviour
{
    Health health;
    Slider healthSlider;
    // Start is called before the first frame update
    void Start()
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
        healthSlider.value = newHealth;
    }
}
