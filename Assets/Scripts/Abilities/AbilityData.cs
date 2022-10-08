using UnityEngine;
using System.Collections.Generic;
using RPG.Combat;

namespace RPG.Abilities
{
    public class AbilityData
    {
        GameObject user;
        Transform castPointProjectile;
        Transform castPointCharacter;
        IEnumerable<GameObject> targets;

        public AbilityData(GameObject user)
        {
            this.user = user;
            this.castPointProjectile = user.GetComponentInChildren<CastPointProjectile>().transform;
            this.castPointCharacter = user.GetComponentInChildren<CastPointCharacter>().transform;
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

        public Transform GetProjectileCastpoint()
        {
            return this.castPointProjectile;
        }
        public Transform GetCharacterCastpoint()
        {
            return this.castPointCharacter;
        }
    }
}
