using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class CanvasFader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetCanvasToOpaque()
        {
            canvasGroup.alpha = 1;
            StartCoroutine(FadeIn(2f));
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                float deltaAlpha = Time.deltaTime / time;
                canvasGroup.alpha += deltaAlpha;
                yield return new WaitForEndOfFrame();
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                float deltaAlpha = Time.deltaTime / time;
                canvasGroup.alpha -= deltaAlpha;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

