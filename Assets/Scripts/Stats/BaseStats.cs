using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] CharacterClass characterClass = CharacterClass.Grunt;

        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
    }
}
