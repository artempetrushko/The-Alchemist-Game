using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu.Inventory
{
    public class MainInventoryCategoryView : InventoryCategoryView
    {
        [SerializeField] private ItemSlotView _itemSlotPrefab;
        [SerializeField] private GameObject _itemSlotsContainer;
        [SerializeField] private ScrollRect _itemSlotsContainerScrollRect;

        public ItemSlotView[] CreateItemSlotViews(int slotsCount)
        {
            var slotViews = new ItemSlotView[slotsCount];
            for (var i = 0; i < slotViews.Length; i++)
            {
                slotViews[i] = Instantiate(_itemSlotPrefab, _itemSlotsContainer.transform);
            }
            return slotViews;
        }

        public void SetItemSlotsContainerScrollRectVerticalPosition(float position) => _itemSlotsContainerScrollRect.verticalNormalizedPosition = position;
    }
}