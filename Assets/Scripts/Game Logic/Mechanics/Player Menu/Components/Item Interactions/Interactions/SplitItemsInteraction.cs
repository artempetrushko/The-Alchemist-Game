using GameLogic.LootSystem;
using GameLogic.PlayerMenu.Inventory;

namespace GameLogic.PlayerMenu
{
    public class SplitItemsInteraction : ItemsInteraction
    {
        private ItemsCountSelectPanelPresenter _itemsCountSelectPanelPresenter;
        private InventoryPresenter _inventoryManager;

        public override void Activate(ItemSlot selectedItemSlot)
        {
            StartItemSlot = selectedItemSlot;

            _itemsCountSelectPanelPresenter.StartItemsCountSelection(selectedItemSlot.ContainedItem.Icon, 1, (selectedItemSlot.ContainedItem as StackableItem).Count.Value - 1, false);
            SubscribeItemsCountSelectPanelEvents();
        }

        public override bool CheckAvailability()
        {
            throw new System.NotImplementedException();
        }

        private void SubscribeItemsCountSelectPanelEvents()
        {
            _itemsCountSelectPanelPresenter.ItemsCountSelected += OnItemsCountSelected;
            _itemsCountSelectPanelPresenter.ItemsCountSelectionCanceled += OnItemsCountSelectionCanceled;
        }

        private void UnsubscribeItemsCountSelectPanelEvents()
        {
            _itemsCountSelectPanelPresenter.ItemsCountSelected -= OnItemsCountSelected;
            _itemsCountSelectPanelPresenter.ItemsCountSelectionCanceled -= OnItemsCountSelectionCanceled;
        }

        private void OnItemsCountSelected(int itemsCount)
        {
            var selectedStackableItem = (StartItemSlot.ContainedItem as StackableItem);
            var newStackableItem = selectedStackableItem.Clone();
            newStackableItem.Count.Value = itemsCount;
            selectedStackableItem.Count.Value -= itemsCount;

            _inventoryManager.TryAddItem(newStackableItem);

            UnsubscribeItemsCountSelectPanelEvents();
        }

        private void OnItemsCountSelectionCanceled()
        {
            UnsubscribeItemsCountSelectPanelEvents();
        }
    }
}