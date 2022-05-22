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
                    return progressionClass.health[level - 1];
                }
            }

            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass classType = CharacterClass.Knight;
            public float[] health;
        }
    }
}