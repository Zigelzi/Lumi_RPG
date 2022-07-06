using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Attributes;

namespace RPG.UI
{
    public class CombatText : MonoBehaviour
    {
        [SerializeField] GameObject damageTextPrefab;
        [SerializeField] float destroyDelay = 2f;


        Health health;

        void Awake()
        {
            health = GetComponentInParent<Health>();  
        }

        void OnEnable()
        {
            health.onDamageTaken += HandleDamageTaken;    
        }

        void HandleDamageTaken(float amount)
        {
            if (damageTextPrefab == null) return;

            GameObject damageTextInstance = Instantiate(damageTextPrefab,
                transform.position, 
                transform.rotation, 
                gameObject.transform);

            DamageText damageText = damageTextInstance.GetComponent<DamageText>();

            if (damageText == null) return;

            damageText.SetText(amount.ToString());

            StartCoroutine(damageText.DestoyDamageText(destroyDelay));
            StartCoroutine(damageText.FadeIn(.5f));
        }

        
    }

}