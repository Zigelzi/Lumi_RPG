using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> characterProgressions;

        public float GetStat(CharacterClass characterClass, Stat stat, int level)
        {
            BuildProgressions();

            if (characterProgressions.ContainsKey(characterClass) && characterProgressions[characterClass].ContainsKey(stat))
            {
                return characterProgressions[characterClass][stat][level - 1];
            }

            return 0;
        }

        void BuildProgressions()
        {
            if (characterProgressions != null) return;

            characterProgressions = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                Dictionary<Stat, float[]> statProgressions = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat stat in progressionClass.stats)
                {
                    statProgressions.Add(stat.type, stat.levels);
                }
                characterProgressions.Add(progressionClass.classType, statProgressions);
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass classType = CharacterClass.Knight;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat type = Stat.Health;
            public float[] levels;
        }
    }
}