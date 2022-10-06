using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Filter_Tag_", menuName = "Abilities/Filtering/Tag filter", order = 2)]
    public class TagFilter: FilterStrategy
    {
        [SerializeField] string tag;
        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (GameObject gameObject in objectsToFilter)
            {
                if (gameObject.gameObject.CompareTag(tag))
                {
                    yield return gameObject;
                }
            }
        }
    }
}
