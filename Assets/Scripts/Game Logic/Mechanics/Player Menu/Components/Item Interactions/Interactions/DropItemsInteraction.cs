using System;
using GameLogic.LootSystem;
using GameLogic.PlayerMenu.Inventory;

namespace GameLogic.PlayerMenu
{
    public class DropItemsInteraction : ItemsInteraction
    {
        private ItemsCountSelectPanelPresenter _itemsCountSelectPanelPresenter;
        private InventoryPresenter _inventoryManager;

        public override void Activate(ItemSlot selectedItemSlot)
        {
            if (selectedItemSlot.ContainedItem is StackableItemState stackableItem)
            {
                StartItemSlot = selectedItemSlot;

                _itemsCountSelectPanelPresenter.StartItemsCountSelection(stackableItem.Icon, 1, stackableItem.Count.Value);
                SubscribeItemsCountSelectPanelEvents();
            }
            else
            {
                _inventoryManager.DropItem(selectedItemSlot);
            }
        }

        public override bool CheckAvailability()
        {
            throw new NotImplementedException();
        }

        private void SubscribeItemsCountSelectPanelEvents()
        {
            _itemsCountSelectPanelPresenter.ItemsCountSelected += OnItemsCountSelected;
            _itemsCountSelectPanelPresenter.AllItemsSelected += OnAllItemsSelected;
            _itemsCountSelectPanelPresenter.ItemsCountSelectionCanceled += OnItemsCountSelectionCanceled;
        }

        private void UnsubscribeItemsCountSelectPanelEvents()
        {
            _itemsCountSelectPanelPresenter.ItemsCountSelected -= OnItemsCountSelected;
            _itemsCountSelectPanelPresenter.AllItemsSelected -= OnAllItemsSelected;
            _itemsCountSelectPanelPresenter.ItemsCountSelectionCanceled -= OnItemsCountSelectionCanceled;
        }

        private void OnItemsCountSelected(int itemsCount)
        {
            _inventoryManager.DropItem(StartItemSlot, itemsCount);
            UnsubscribeItemsCountSelectPanelEvents();
        }

        private void OnAllItemsSelected()
        {
            _inventoryManager.DropItem(StartItemSlot);
            UnsubscribeItemsCountSelectPanelEvents();
        }

        private void OnItemsCountSelectionCanceled()
        {
            UnsubscribeItemsCountSelectPanelEvents();
        }
    }
}