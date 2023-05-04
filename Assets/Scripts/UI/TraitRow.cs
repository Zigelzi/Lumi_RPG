using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using RPG.Stats;

namespace RPG.UI
{
    public class TraitRow : MonoBehaviour
    {
        [SerializeField] Button _minusButton;
        [SerializeField] Button _plusButton;
        [SerializeField] Trait _traitType;
        [SerializeField] TMP_Text _traitText;
        [SerializeField] TMP_Text _traitTitle;
        int _unassignedPoints = 0;

        void Awake()
        {
            _traitText.text = _unassignedPoints.ToString();
            _traitTitle.text = _traitType.ToString();
        }

        void OnEnable()
        {
            if (_minusButton == null || _plusButton == null) return;
            _minusButton.onClick.AddListener(DecrementAllocation);
            _plusButton.onClick.AddListener(IncrementAllocation);
        }

        void Update()
        {
            _minusButton.interactable = _unassignedPoints > 0;    
        }

        void OnDisable()
        {
            if (_minusButton == null || _plusButton == null) return;
            _minusButton.onClick.RemoveListener(DecrementAllocation);
            _plusButton.onClick.RemoveListener(IncrementAllocation);
        }


        void Allocate(int points)
        {
            if (points < 0 && _unassignedPoints <= 0) return;

            _unassignedPoints += points;
            _traitText.text = _unassignedPoints.ToString();
        }

        void DecrementAllocation() => Allocate(-1);
        void IncrementAllocation() => Allocate(1);
    }
}
