using Controls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsContainerMenuView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _containerTitle;
        [SerializeField] private ContainedItemView _containedItemViewPrefab;
        [SerializeField] private GameObject _containedItemViewsContainer;
        [SerializeField] private ScrollRect _containerScrollRect;
        [SerializeField] private ControlsTipsSectionView _controlsTipsSectionView;

        public ControlsTipsSectionView ControlsTipsSectionView => _controlsTipsSectionView;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetContainerTitleText(string text) => _containerTitle.text = text;

        public void SetScrollRectVerticalNormalizedPosition(float verticalNormalizedPosition) => _containerScrollRect.verticalNormalizedPosition = verticalNormalizedPosition;

        public ContainedItemView GetContainedItemViewByIndex(int index)
        {
            if (_containedItemViewsContainer.transform.childCount <= index)
            {
                while (_containedItemViewsContainer.transform.childCount <= index)
                {
                    Instantiate(_containedItemViewPrefab, _containedItemViewsContainer.transform);
                }
            }
            return _containedItemViewsContainer.transform.GetChild(index).GetComponent<ContainedItemView>();
        }

        public void DisableAllContainedItemViews()
        {
            foreach (var containedItemView in _containedItemViewsContainer.GetComponentsInChildren<ContainedItemView>())
            {
                containedItemView.SetActive(false);
            }
        }
    }
}