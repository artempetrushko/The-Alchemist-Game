using Controls;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class PlayerMenuNavigationPanelView : MonoBehaviour
    {
        [SerializeField] private PlayerMenuSectionSelectButton _sectionButtonPrefab;
        [SerializeField] private GameObject _sectionButtonsContainer;
        [Space]
        [SerializeField] private ControlTipView _leftSwitchSectionTipView;
        [SerializeField] private ControlTipView _rightSwitchSectionTipView;

        public ControlTipView LeftSwitchSectionTipView => _leftSwitchSectionTipView;
        public ControlTipView RightSwitchSectionTipView => _rightSwitchSectionTipView;

        public PlayerMenuSectionSelectButton CreateSectionSelectButton() => Instantiate(_sectionButtonPrefab, _sectionButtonsContainer.transform);
    }
}