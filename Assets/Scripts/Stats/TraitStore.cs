using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Stats
{
    public class TraitStore : MonoBehaviour
    {
        // TODO: Player can GAIN X trait points when leveling up
        // TODO: Player can COMMIT points to increase the traits permanently
        // TODO: Player can increase their stats when traits are committed
        [SerializeField] int _availablePoints = 5;

        Dictionary<Trait, int> _assignedTraits = new Dictionary<Trait, int>();
        Dictionary<Trait, int> _stagedTraits = new Dictionary<Trait, int>();

        public UnityEvent onTraitAssigned;

        public int AvailablePoints { get { return _availablePoints; } }

        public int GetPoints(Trait trait)
        {
            return _assignedTraits.ContainsKey(trait) ? _assignedTraits[trait] : 0;
        }

        public void Assign(Trait trait, int amount)
        {

            if (_assignedTraits.ContainsKey(trait))
            {
                if (_assignedTraits[trait] + amount < 0) return;

                _assignedTraits[trait] += amount;
            }
            else
            {
                _assignedTraits.Add(trait, amount);
            }

            _availablePoints += -amount;
            onTraitAssigned?.Invoke();
        }


    }

}