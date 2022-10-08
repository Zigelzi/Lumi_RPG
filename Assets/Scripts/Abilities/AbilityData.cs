using UnityEngine;
using System.Collections.Generic;

namespace RPG.Abilities
{
    public class AbilityData
    {
        GameObject user;
        IEnumerable<GameObject> targets;

        float cooldown = 2f;

        public AbilityData(GameObject user)
        {
            this.user = user;
        }

        public void SetTargets(IEnumerable<GameObject> newTargets)
        {
            this.targets = newTargets;
        }

        public IEnumerable<GameObject> GetTargets()
        {
            return this.targets;
        }

        public GameObject GetUser()
        {
            return this.user;
        }
    }
}
