using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
    public class FloatingText : MonoBehaviour
    {
        Animation textAnimation;
        TMP_Text floatingText;

        void Awake()
        {
            floatingText = GetComponentInChildren<TMP_Text>();
            textAnimation = GetComponent<Animation>();
        }

        void Update()
        {
            if (!textAnimation.isPlaying)
            {
                Destroy(gameObject);
            }
        }

        public void SetText(float amount)
        {
            floatingText.text = amount.ToString();
        }

        public void SetText(string text)
        {
            floatingText.text = text;
        }
    }
}

