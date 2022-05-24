using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Stats;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float currentExperience = 0;

        public void AddExperience(float amount)
        {
            currentExperience += amount;
        }
    }
}

