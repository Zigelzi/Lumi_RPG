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
            damageText = GetComponentInChildren<TMP_Text>();    
        }

        public void SetText(float amount)
        {
            damageText.text = amount.ToString();
        }
    }
}

