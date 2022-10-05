using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Filter_Tag", menuName = "Abilities/Filtering/Tag filter", order = 2)]
    public class TagFilter: FilterStrategy
    {
        [SerializeField] string tag;
        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (GameObject objectToFilter in objectsToFilter)
            {
                if (objectToFilter.gameObject.tag == tag)
                {
                    yield return objectToFilter;
                }
            }
        }
    }
}
