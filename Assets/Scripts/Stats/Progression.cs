using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        public float GetStat(CharacterClass characterClass, Stat stat, int level)
        {
            ProgressionStat progressionStat;

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.classType == characterClass)
                {
                    progressionStat = progressionClass.GetStat(stat);

                    if (progressionStat == null) return 0;
                    if (progressionStat.levels.Length < level) return 0;

                    return progressionStat.levels[level - 1];
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
            public float[] levels;
        }
    }
}