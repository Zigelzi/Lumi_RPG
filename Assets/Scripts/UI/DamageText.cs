using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        TMP_Text damageText;

        void Awake()
        {
            damageText = GetComponent<TMP_Text>();    
        }

        public void SetText(string text)
        {
            damageText.text = text;
        }

        public IEnumerator DestoyDamageText(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }

        public IEnumerator DisplayText(float fadeInDuration, float displayDuration, float fadeOutDuration)
        {
            yield return FadeIn(fadeInDuration);
            yield return new WaitForSeconds(displayDuration);
            yield return FadeOut(fadeOutDuration);
            Destroy(gameObject);
        }

        IEnumerator FadeIn(float duration)
        {
            while (damageText.color.a < 1)
            {
                float deltaAlpha = Time.deltaTime / duration;
                float newOpacity = damageText.color.a + deltaAlpha;
                Color newTextColor = new Color(damageText.color.r,
                    damageText.color.g, 
                    damageText.color.b, 
                    newOpacity);
                
                damageText.color = newTextColor;
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator FadeOut(float duration)
        {
            while (damageText.color.a > 0)
            {
                float deltaAlpha = Time.deltaTime / duration;
                float newOpacity = damageText.color.a - deltaAlpha;
                Color newTextColor = new Color(damageText.color.r,
                    damageText.color.g,
                    damageText.color.b,
                    newOpacity);

                damageText.color = newTextColor;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

