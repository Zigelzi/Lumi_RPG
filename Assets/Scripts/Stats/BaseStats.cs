using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour, ISaveable
    {
        [SerializeField] CharacterClass characterClass = CharacterClass.Knight;

        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] int currentLevel = 1;
        [SerializeField] Progression progression;
        [SerializeField] GameObject levelUpVFX;

        public int StartingLevel { get { return startingLevel; } }
        public int CurrentLevel { get { return currentLevel; } }

        public event Action<int> onLevelChange;

        void Awake()
        {
            currentLevel = startingLevel;    
        }

        public float GetStartingStat(Stat statType)
        {
            return progression.GetStat(characterClass, statType, startingLevel);
        }

        public float GetStat(Stat statType, int level)
        {
            return progression.GetStat(characterClass, statType, level);
        }

        public void LevelUp()
        {
            currentLevel += 1;
            onLevelChange?.Invoke(currentLevel);
            SpawnLevelUpVFX();
        }

        public object CaptureState()
        {
            return currentLevel;
        }

        public void RestoreState(object state)
        {
            int restoredLevel = (int)state;
            
            currentLevel = restoredLevel;
        }

        void SpawnLevelUpVFX()
        {
            if (levelUpVFX == null) return;

            Instantiate(levelUpVFX, transform);
        }
    }
}
