using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.classType == characterClass)
                {
                    return progressionClass.GetStat(Stat.Health).levels[level];
                }
            }

            return 0;
        }

        public int GetExperienceReward(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.classType == characterClass)
                {
                    return progressionClass.GetStat(Stat.ExperienceReward).levels[level];
                }
            }

            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass classType = CharacterClass.Knight;
            public ProgressionStat[] stats;

            public ProgressionStat GetStat(Stat statType)
            {
                foreach (ProgressionStat stat in stats)
                {
                    if (stat.type == statType)
                    {
                        return stat;
                    }
                }

                return null;
            }
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat type = Stat.Health;
            public int[] levels;
        }
    }
}