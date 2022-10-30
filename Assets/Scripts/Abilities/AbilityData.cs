using UnityEngine;
using System.Collections.Generic;
using RPG.Combat;
using System.Collections;
using System.Linq;

namespace RPG.Abilities
{
    public class AbilityData
    {
        GameObject user;
        Transform castPointProjectile;
        Transform castPointCharacter;
        IEnumerable<GameObject> targets;


        public AbilityData(GameObject newUser)
        {
            this.user = newUser;
            this.castPointProjectile = newUser.GetComponentInChildren<CastPointProjectile>().transform;
            this.castPointCharacter = newUser.GetComponentInChildren<CastPointCharacter>().transform;
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

        public void StartCoroutine(IEnumerator coroutine)
        {
            user.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
        }
    }
}
