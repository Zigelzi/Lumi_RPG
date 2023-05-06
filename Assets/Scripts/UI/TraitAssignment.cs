using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Stats;
using UnityEngine.UI;

namespace RPG.UI
{
    public class TraitAssignment : MonoBehaviour
    {
        // Player can view how many unassigned points they have
        // When available traits update, update  the trait text content
        [SerializeField] TMP_Text _unassignedTraitsText;
        [SerializeField] Button _assignTraitsButton;
        TraitStore _traitStore;

        void Awake()
        {
            _traitStore = FindObjectOfType<TraitStore>();
        }

        void Start()
        {
            if (_traitStore == null) return;
            _unassignedTraitsText.text = _traitStore.UnassignedPoints.ToString();
        }

        void OnEnable()
        {
            if (_traitStore == null) return;
            _traitStore.onTraitAssigned.AddListener(UpdateUnassignedPoints);

            if (_assignTraitsButton == null) return;
            _assignTraitsButton.onClick.AddListener(AssignTraits);
        }

        void OnDisable()
        {
            if (_traitStore == null) return;
            _traitStore.onTraitAssigned.RemoveListener(UpdateUnassignedPoints);

            if (_assignTraitsButton == null) return;
            _assignTraitsButton.onClick.AddListener(AssignTraits);
        }

        void UpdateUnassignedPoints()
        {
            if (_unassignedTraitsText == null) return;

            _unassignedTraitsText.text = _traitStore.UnassignedPoints.ToString();
        }

        void AssignTraits()
        {
        }
    }
}
