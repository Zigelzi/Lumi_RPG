using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Stats
{
    public class TraitStore : MonoBehaviour
    {
        // TODO: Player can increase their stats when traits are committed
        [SerializeField] int _unassignedPoints = 0;

        Dictionary<Trait, int> _assignedTraits = new Dictionary<Trait, int>();
        Dictionary<Trait, int> _stagedTraits = new Dictionary<Trait, int>();

        BaseStats _baseStats;

        public UnityEvent onTraitAssigned;

        public int UnassignedPoints { get { return _unassignedPoints; } }

        void Awake()
        {
            _baseStats = GetComponent<BaseStats>();
        }

        void OnEnable()
        {
            _baseStats.onLevelChange += HandleLevelChange;
        }

        void Start()
        {
            _unassignedPoints = GetUnassignedPoints();
        }

        void OnDisable()
        {
            _baseStats.onLevelChange -= HandleLevelChange;
        }

        public int GetProposedPoints(Trait trait)
        {
            return GetPoints(trait) + GetStagedPoints(trait);
        }

        public int GetPoints(Trait trait)
        {
            return _assignedTraits.ContainsKey(trait) ? _assignedTraits[trait] : 0;
        }

        public int GetStagedPoints(Trait trait)
        {
            return _stagedTraits.ContainsKey(trait) ? _stagedTraits[trait] : 0;
        }

        public void Assign(Trait trait, int amount)
        {

            if (!CanAssignPoints(trait, amount)) return;

            _stagedTraits[trait] = GetStagedPoints(trait) + amount;
            _unassignedPoints = GetUnassignedPoints();
            onTraitAssigned?.Invoke();
        }

        public bool CanAssignPoints(Trait trait,int amount)
        {
            if (GetStagedPoints(trait) + amount < 0) return false;
            if (GetUnassignedPoints() < amount) return false;

            return true;
        }

        public int GetAssignablePoints()
        {
            return (int)GetComponent<BaseStats>().GetStat(Stat.TotalTraitPoints);
        }

        public void Commit()
        {
            foreach(Trait trait in _stagedTraits.Keys)
            {
                _assignedTraits[trait] = GetProposedPoints(trait);
            }

            _stagedTraits.Clear();
            onTraitAssigned?.Invoke();
        }

        void HandleLevelChange(int newLevel)
        {
            _unassignedPoints = GetUnassignedPoints();
            onTraitAssigned.Invoke();
        }

        int GetUnassignedPoints()
        {
            if (_baseStats == null) return 0;

            return GetAssignablePoints() - GetTotalProposedPoints();
        }

        int GetTotalProposedPoints()
        {
            int totalPoints = 0;

            foreach (int assignedPoints in _assignedTraits.Values)
            {
                totalPoints += assignedPoints;
            }

            foreach (int stagedPoints in _stagedTraits.Keys)
            {
                totalPoints += stagedPoints;
            }


            return totalPoints;
        }

    }

}