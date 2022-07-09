using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Attributes;

namespace RPG.UI
{
    public class CombatText : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;
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

            DamageText damageTextInstance = Instantiate<DamageText>(damageTextPrefab,
                transform.position, 
                transform.rotation, 
                gameObject.transform);

            if (damageTextInstance == null) return;

            damageTextInstance.SetText(amount.ToString());

        }
    }

}