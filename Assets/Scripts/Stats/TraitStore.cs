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
        [SerializeField] int _unassignedPoints = 5;

        Dictionary<Trait, int> _assignedTraits = new Dictionary<Trait, int>();
        Dictionary<Trait, int> _stagedTraits = new Dictionary<Trait, int>();

        public UnityEvent onTraitAssigned;

        public int UnassignedPoints { get { return _unassignedPoints; } }

        public int GetPoints(Trait trait)
        {
            return _assignedTraits.ContainsKey(trait) ? _assignedTraits[trait] : 0;
        }

        public void Assign(Trait trait, int amount)
        {

            if (!CanAssignPoints(trait, amount)) return;

            _assignedTraits[trait] = GetPoints(trait) + amount;
            _unassignedPoints -= amount;
            onTraitAssigned?.Invoke();
        }

        public bool CanAssignPoints(Trait trait,int amount)
        {
            if (GetPoints(trait) + amount < 0) return false;
            if (_unassignedPoints < amount) return false;

            return true;
        }
    }

}