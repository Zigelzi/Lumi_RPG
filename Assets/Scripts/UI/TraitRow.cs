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
        [SerializeField] TMP_Text _traitCurrentValue;
        [SerializeField] TMP_Text _traitTitle;
        TraitStore _traitStore;

        void Awake()
        {
            _traitTitle.text = _traitType.ToString();
        }

        void Start()
        {
            _traitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();

            if (_traitStore == null) return;

            _traitCurrentValue.text = _traitStore.GetPoints(_traitType).ToString();
        }

        void OnEnable()
        {
            if (_minusButton == null || _plusButton == null) return;
            _minusButton.onClick.AddListener(DecrementAllocation);
            _plusButton.onClick.AddListener(IncrementAllocation);
        }

        void Update()
        {
            _minusButton.interactable = _traitStore.CanAssignPoints(_traitType, -1);
            _plusButton.interactable = _traitStore.CanAssignPoints(_traitType, 1);
        }

        void OnDisable()
        {
            if (_minusButton == null || _plusButton == null) return;
            _minusButton.onClick.RemoveListener(DecrementAllocation);
            _plusButton.onClick.RemoveListener(IncrementAllocation);
        }


        void Allocate(int points)
        {
            _traitStore.Assign(_traitType, points);
            _traitCurrentValue.text = _traitStore.GetPoints(_traitType).ToString();
        }

        void DecrementAllocation() => Allocate(-1);
        void IncrementAllocation() => Allocate(1);
    }
}
