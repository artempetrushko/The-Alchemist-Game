using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Environment
{
    public class ItemsContainerContentPanelController
    {
        private ItemsContainerContentPanelView _itemsContainerContentPanelView;

        private ContainedItemView containedItemViewPrefab;

        public ItemsContainerContentPanelController(ItemsContainerContentPanelView itemsContainerContentPanelView)
        {
            _itemsContainerContentPanelView = itemsContainerContentPanelView;
        }

        public void SetInfo(string containerName) => _itemsContainerContentPanelView.SetContainerTitleText(containerName);

        public List<(ItemState item, ContainedItemView itemView)> CreateContainedItemViews(List<ItemState> spawnedItems, Action<(ItemState, ContainedItemView)> pickItemFunction, Action<(ItemState, ContainedItemView)> itemViewHoveredAction)
        {
            var containingItems = new List<(ItemState item, ContainedItemView itemView)>();
            foreach (var item in spawnedItems)
            {
                var itemView = Instantiate(containedItemViewPrefab, containedItemViewsContainer.transform);
                var containingItem = (item, itemView);
                itemView.SetInfo(item, () => pickItemFunction(containingItem));
                itemView.AddEventTriggerListener(EventTriggerType.PointerEnter, (eventData) => { itemViewHoveredAction(containingItem); });
                containingItems.Add(containingItem);
            }
            return containingItems;
        }
    }

    public class ItemsContainerContentPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _containerTitle;
        [SerializeField] private GameObject _containedItemViewsContainer;
        [SerializeField] private ScrollRect containerScrollRect;
        [SerializeField] private ControlsTipsSectionView controlsTipsSectionView;

        public GameObject ContainedItemViewsContainer => _containedItemViewsContainer;

        public ControlsTipsSectionView ControlsTipsSectionView => controlsTipsSectionView;

        public void SetContainerTitleText(string text) => _containerTitle.text = text;

        public void SelectContainedItemView(ContainedItemView itemView, float containerViewScrollPosition)
        {
            itemView.Select();
            containerScrollRect.verticalNormalizedPosition = containerViewScrollPosition;
        }

        
    }
}