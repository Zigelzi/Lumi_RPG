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
                    return progressionClass.stats[0].levels[level];
                }
            }

            return 0;
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
            public Stat stat = Stat.Health;
            public int[] levels;
        }
    }
}