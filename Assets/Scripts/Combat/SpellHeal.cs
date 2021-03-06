using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class SpellHeal : MonoBehaviour
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

