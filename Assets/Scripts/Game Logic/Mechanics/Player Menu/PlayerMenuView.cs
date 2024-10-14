using Controls;
using GameLogic.PlayerMenu;
using UnityEngine;

namespace GameLogic
{
    public class PlayerMenuView : MonoBehaviour
    {
        [SerializeField] private PlayerMenuNavigationPanelView _navigationPanelView;
        [SerializeField] private ControlsTipsSectionView _controlsTipsSectionView;

        public PlayerMenuNavigationPanelView NavigationPanelView => _navigationPanelView;
        public ControlsTipsSectionView ControlsTipsSectionView => _controlsTipsSectionView;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}