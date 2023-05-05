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
        int _assignedPoints = 0;
        TraitStore _traitStore;

        void Awake()
        {
            _traitStore = FindObjectOfType<TraitStore>();
            _traitText.text = _assignedPoints.ToString();
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
            _minusButton.interactable = _assignedPoints > 0;
            _plusButton.interactable = _traitStore.AvailablePoints > 0;
        }

        void OnDisable()
        {
            if (_minusButton == null || _plusButton == null) return;
            _minusButton.onClick.RemoveListener(DecrementAllocation);
            _plusButton.onClick.RemoveListener(IncrementAllocation);
        }


        void Allocate(int points)
        {
            if (points < 0 && _assignedPoints <= 0) return;
            if (points > 0 && _traitStore.AvailablePoints <= 0) return;

            _assignedPoints += points;

            // Positive numbers consume available points
            // and negative numbers increase the available points
            _traitStore.UpdatePoints(-points); 
            _traitText.text = _assignedPoints.ToString();
        }

        void DecrementAllocation() => Allocate(-1);
        void IncrementAllocation() => Allocate(1);
    }
}
