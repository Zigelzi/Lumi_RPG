using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Spells
{
    public class Spell_Heal : MonoBehaviour
    {
        [SerializeField] float spellLifetime = 3f;
        // Start is called before the first frame update
        void Start()
        {
            Invoke(nameof(DestroySpell), spellLifetime);
        }

        void DestroySpell()
        {
            Destroy(gameObject);
        }
    }
}

