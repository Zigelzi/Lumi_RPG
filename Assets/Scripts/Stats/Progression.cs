using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass classType = CharacterClass.Knight;
            [SerializeField] float[] health;
        }
    }
}