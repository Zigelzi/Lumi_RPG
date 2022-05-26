﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] CharacterClass characterClass = CharacterClass.Knight;

        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] int currentLevel = 1;
        [SerializeField] Progression progression;

        public int StartingLevel { get { return startingLevel; } }
        public int CurrentLevel { get { return currentLevel; } }

        void Start()
        {
            currentLevel = startingLevel;    
        }

        public float GetStartingStat(Stat statType)
        {
            return progression.GetStat(characterClass, statType, startingLevel);
        }

        public void LevelUp()
        {
            currentLevel += 1;
        }
    }
}
