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
        [SerializeField] int currentLevel = 0;
        [SerializeField] bool isUsingModifiers = false;
        [SerializeField] Progression progression;
        [SerializeField] GameObject levelUpVFX;

        public int StartingLevel { get { return startingLevel; } }
        public int CurrentLevel { get { return currentLevel; } }

        public event Action<int> onLevelChange;

        void Awake()
        {
            currentLevel = GetLevel();    
        }

        public float GetStat(Stat statType)
        {
            float baseStatValue = GetBaseStat(statType);
            float additiveStatValue = GetAdditiveModifiers(statType);
            float percentageStatValue = GetPercentageModifiers(statType);

            float totalStatValue = (baseStatValue + additiveStatValue) * percentageStatValue;

            return totalStatValue;
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

        float GetBaseStat(Stat statType)
        {
            return progression.GetStat(characterClass, statType, GetLevel());
        }

        float GetAdditiveModifiers(Stat statType)
        {
            if (!isUsingModifiers) return 0;

            float totalStatValue = 0;
            foreach (IStatModifier statModifier in GetComponents<IStatModifier>())
            {
                foreach (float modifierValue in statModifier.GetAdditiveModifier(statType))
                {
                    totalStatValue += modifierValue;
                }
            }

            return totalStatValue;
        }

        float GetPercentageModifiers(Stat statType)
        {
            if (!isUsingModifiers) return 1;

            float totalPercentageMultiplier = 0;
            foreach (IStatModifier statModifier in GetComponents<IStatModifier>())
            {
                foreach (float modifierValue in statModifier.GetPercentageModifier(statType))
                {
                    totalPercentageMultiplier += modifierValue;
                }
            }

            return 1 + totalPercentageMultiplier / 100;
        }

        void SpawnLevelUpVFX()
        {
            if (levelUpVFX == null) return;

            Instantiate(levelUpVFX, transform);
        }

        int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = startingLevel;
            }

            return currentLevel;
        }
    }
}
