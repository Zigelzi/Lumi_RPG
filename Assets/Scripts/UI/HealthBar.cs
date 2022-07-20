using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Attributes;

public class HealthBar : MonoBehaviour
{
    [SerializeField][Range(0, 2f)] float fadeDuration = .5f;
    [SerializeField][Range(0, 10f)] float fadeOutDelay = 2f;

    CanvasGroup canvasGroup;
    Health health;
    Slider healthSlider;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        health = GetComponentInParent<Health>();
        healthSlider = GetComponentInChildren<Slider>();

        canvasGroup.alpha = 0;
    }

    void OnEnable()
    {
        health.onHealthChange.AddListener(HandleHealthUpdate);
        health.onUnitDeath.AddListener(HandleUnitDeath);
    }

    void Start()
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = health.CurrentHealth;
    }

    void OnDisable()
    {
        health.onHealthChange.RemoveListener(HandleHealthUpdate);
        health.onUnitDeath.RemoveListener(HandleUnitDeath);
    }

    void HandleHealthUpdate(float newHealth)
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = newHealth;

        if (Mathf.Approximately(health.CurrentHealth, health.MaxHealth))
        {
            StartCoroutine(FadeToTransparent(true));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FadeToOpaque());
        }
    }

    void HandleUnitDeath()
    {
        StartCoroutine(FadeToTransparent(false));
    }

    IEnumerator FadeToTransparent(bool isDelayed)
    {
        Debug.Log($"Fading to transparent started and fade is delayed: {isDelayed}");
        if (isDelayed)
        {
            yield return new WaitForSeconds(fadeOutDelay);
        }
        Debug.Log("Starting to fade out");
        Debug.Log($"Canvasgroup alpha is bigger than 0: {canvasGroup.alpha > 0}");
        while (canvasGroup.alpha > 0)
        {
            float deltaAlpha = Time.deltaTime / fadeDuration;

            Debug.Log($"DeltaAlpha: {deltaAlpha}");
            Debug.Log($"Canvas group alpha before {canvasGroup.alpha}");
            canvasGroup.alpha -= deltaAlpha;
            Debug.Log($"Canvas group alpha after {canvasGroup.alpha}");
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Finished to fade out");
    }

    IEnumerator FadeToOpaque()
    {
        while (canvasGroup.alpha < 1)
        {
            float deltaAlpha = Time.deltaTime / fadeDuration;
            canvasGroup.alpha += deltaAlpha;
            yield return new WaitForEndOfFrame();
        }
    }
}
