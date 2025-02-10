using Controls;
using TMPro;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public class EnvironmentInteractionPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _interactiveObjectTitle;
        [SerializeField] private ControlsTipsSectionView _controlsTipsSectionView;

        public ControlsTipsSectionView ControlsTipsSectionView => _controlsTipsSectionView;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetInteractiveObjectName(string name) => _interactiveObjectTitle.text = name;
    }
}