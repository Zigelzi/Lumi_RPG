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
        [SerializeField] Button _commitTraitsButton;
        TraitStore _traitStore;

        void Start()
        {
            _traitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();
            if (_traitStore == null) return;
            _unassignedTraitsText.text = _traitStore.UnassignedPoints.ToString();
            _traitStore.onTraitAssigned.AddListener(UpdateUnassignedPoints);

            if (_commitTraitsButton == null) return;
            _commitTraitsButton.onClick.AddListener(_traitStore.Commit);
        }

        void OnDisable()
        {
            if (_traitStore == null) return;
            _traitStore.onTraitAssigned.RemoveListener(UpdateUnassignedPoints);

            if (_commitTraitsButton == null) return;
            _commitTraitsButton.onClick.RemoveListener(_traitStore.Commit);
        }

        void UpdateUnassignedPoints()
        {
            if (_unassignedTraitsText == null) return;

            _unassignedTraitsText.text = _traitStore.UnassignedPoints.ToString();
        }
    }
}
