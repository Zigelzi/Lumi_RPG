using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Spells/Create New Spell", order = 0)]
    public class SpellConfig : ScriptableObject
    {
        [SerializeField] GameObject vfx;
        
    }
}
