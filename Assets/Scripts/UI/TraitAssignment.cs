using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using RPG.Stats;

namespace RPG.UI
{
    public class TraitAssignment : MonoBehaviour
    {
        // Player can view how many unassigned points they have
        // When available traits update, update  the trait text content
        [SerializeField] TMP_Text _unassignedTraitsText;
        TraitStore _traitStore;

        void Awake()
        {
            _traitStore = FindObjectOfType<TraitStore>();

            if (_traitStore == null) return;
            _unassignedTraitsText.text = _traitStore.AvailablePoints.ToString();
        }

        void OnEnable()
        {
            _traitStore.onTraitUpdate.AddListener(UpdateUnassignedPoints);
        }

        void OnDisable()
        {
            _traitStore.onTraitUpdate.RemoveListener(UpdateUnassignedPoints);
        }

        void UpdateUnassignedPoints()
        {
            if (_unassignedTraitsText == null) return;

            _unassignedTraitsText.text = _traitStore.AvailablePoints.ToString();
        }
    }
}
