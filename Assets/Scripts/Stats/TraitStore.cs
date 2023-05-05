using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class TraitStore : MonoBehaviour
    {
        // TODO: Player can GAIN X trait points when leveling up
        // TODO: Player can ALLOCATE the available points to any trait
        // TODO: Player can COMMIT points to increase the traits permanently
        // TODO: Player can increase their stats when traits are committed
        [SerializeField] int _availablePoints = 0;

        public int AvailablePoints { get { return _availablePoints; } }

        public void UpdatePoints(int amount)
        {
            if (_availablePoints + amount < 0) return;

            _availablePoints += amount;
        }
    }

}