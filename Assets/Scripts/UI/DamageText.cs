using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        Animation textAnimation;
        TMP_Text damageText;

        void Awake()
        {
            damageText = GetComponentInChildren<TMP_Text>();
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
            damageText.text = amount.ToString();
        }
    }
}

