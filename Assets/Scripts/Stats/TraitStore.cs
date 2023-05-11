using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Stats
{
    public class TraitStore : MonoBehaviour, IStatModifier
    {
        // TODO: Player can increase their stats when traits are committed
        [SerializeField] int _unassignedPoints = 0;
        [SerializeField] TraitBonus[] _bonusConfig;

        Dictionary<Trait, int> _assignedTraits = new Dictionary<Trait, int>();
        Dictionary<Trait, int> _stagedTraits = new Dictionary<Trait, int>();

        Dictionary<Stat, Dictionary<Trait, float>> _additiveBonusCache;
        Dictionary<Stat, Dictionary<Trait, float>> _percentageBonusCache;

        BaseStats _baseStats;

        [System.Serializable]
        class TraitBonus
        {
            public Trait trait = Trait.Intelligence;
            public Stat stat = Stat.Attunement;
            public float additiveBonusPerPoint = 0;
            public float percentageBonusPerPoint = 0;
        }

        public UnityEvent onTraitAssigned;

        public int UnassignedPoints { get { return _unassignedPoints; } }

        void Awake()
        {
            _baseStats = GetComponent<BaseStats>();
            BuildBonusCaches();
        }

        void OnEnable()
        {
            _baseStats.onLevelChange += HandleLevelChange;
        }

        void Start()
        {
            _unassignedPoints = GetUnassignedPoints();
            onTraitAssigned?.Invoke();
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
            // FIX: Bonuses are not applied when committing them
            foreach (Trait trait in _stagedTraits.Keys)
            {
                _assignedTraits[trait] = GetProposedPoints(trait);
            }

            _stagedTraits.Clear();
            onTraitAssigned?.Invoke();
        }

        public IEnumerable<float> GetAdditiveModifier(Stat stat)
        {
            if (!_additiveBonusCache.ContainsKey(stat)) yield break;

            foreach (Trait trait in _additiveBonusCache[stat].Keys)
            {
                float bonus = _additiveBonusCache[stat][trait];

                Debug.Log($"Added {bonus} points to {trait} for {stat}");

                yield return bonus * GetPoints(trait);
            }
        }

        public IEnumerable<float> GetPercentageModifier(Stat stat)
        {
            if (!_percentageBonusCache.ContainsKey(stat)) yield break;

            foreach (Trait trait in _percentageBonusCache[stat].Keys)
            {
                float bonus = _percentageBonusCache[stat][trait];

                yield return bonus * GetPoints(trait);
            }
        }

        void BuildBonusCaches()
        {
            _additiveBonusCache = new Dictionary<Stat, Dictionary<Trait, float>>();
            _percentageBonusCache = new Dictionary<Stat, Dictionary<Trait, float>>();

            foreach (TraitBonus bonus in _bonusConfig)
            {

                if (!_additiveBonusCache.ContainsKey(bonus.stat))
                {
                    Dictionary<Trait, float> additiveBonus = BuildAdditiveBonus(bonus);
                    _additiveBonusCache.Add(bonus.stat, additiveBonus);

                }
                
                if (!_percentageBonusCache.ContainsKey(bonus.stat))
                {
                    Dictionary<Trait, float> percentageBonus = BuildPercentageBonus(bonus);
                    _percentageBonusCache.Add(bonus.stat, percentageBonus);
                }

            }

        }

        Dictionary<Trait, float> BuildAdditiveBonus(TraitBonus bonus)
        {
            Dictionary<Trait, float> additiveBonus = new Dictionary<Trait, float>();
            additiveBonus.Add(bonus.trait, bonus.additiveBonusPerPoint);

            return additiveBonus;
        }

        Dictionary<Trait, float> BuildPercentageBonus(TraitBonus bonus)
        {
            Dictionary<Trait, float> percentageBonus = new Dictionary<Trait, float>();
            percentageBonus.Add(bonus.trait, bonus.percentageBonusPerPoint);

            return percentageBonus;
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

            foreach (int stagedPoints in _stagedTraits.Values)
            {
                totalPoints += stagedPoints;
            }


            return totalPoints;
        }
    }

}