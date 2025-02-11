using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.LootSystem
{
    public class ItemsContainerContentView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text containerTitle;
        [SerializeField]
        private ContainedItemView containedItemViewPrefab;
        [SerializeField]
        private GameObject containedItemViewsContainer;
        [SerializeField]
        private ScrollRect containerScrollRect;
        [SerializeField]
        private ControlsTipsSectionView controlsTipsSectionView;

        public ControlsTipsSectionView ControlsTipsSectionView => controlsTipsSectionView;

        public void SetInfo(string containerName)
        {
            containerTitle.text = containerName;
        }

        public void SelectContainedItemView(ContainedItemView itemView, float containerViewScrollPosition)
        {
            itemView.Select();
            containerScrollRect.verticalNormalizedPosition = containerViewScrollPosition;
        }

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
}