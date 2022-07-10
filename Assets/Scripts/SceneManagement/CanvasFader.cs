using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class CanvasFader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine ongoingRoutine;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetCanvasToTransparent()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;

            StartCoroutine(Fade(0, 2f));
        }

        public IEnumerator FadeToTransparent(float duration)
        {
            return Fade(0f, duration);
        }

        public IEnumerator FadeToOpaque(float duration)
        {
            return Fade(1f, duration);
        }

        IEnumerator Fade(float targetAlpha, float duration)
        {
            if (ongoingRoutine != null)
            {
                StopCoroutine(ongoingRoutine);
            }
            ongoingRoutine = StartCoroutine(FadeRoutine(targetAlpha, duration));
            yield return ongoingRoutine;
        }

        IEnumerator FadeRoutine(float targetAlpha, float duration)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
            {
                float deltaAlpha = Time.deltaTime / duration;
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha,
                    targetAlpha,
                    deltaAlpha);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

