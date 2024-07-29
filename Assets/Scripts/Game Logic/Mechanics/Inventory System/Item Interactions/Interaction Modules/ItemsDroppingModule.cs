using System;

namespace GameLogic.Inventory
{
    public class ItemsDroppingModule : ItemsInteractionModule, IInteractionCancelable
    {
        private ItemsCountSelectPanelController itemsCountChoiceManager;
        private InventoryManager inventoryManager;

        public override void StartInteraction(ItemSlot selectedItemSlot)
        {
            if (selectedItemSlot.BaseItemState is StackableItemState stackableItem)
            {
                itemsCountChoiceManager.CreateItemsCountChoiceView("Выберите количество предметов", stackableItem, 1, stackableItem.ItemsCount, new ItemsCountChoiceData()
                {
                    ConfirmAction = new ItemsCountChoiceAction("Выбросить", () => inventoryManager.DropItem(stackableItem, itemsCountChoiceManager.SelectedItemsCount)),
                    ConfirmAllAction = new ItemsCountChoiceAction("Выбросить всё", () => inventoryManager.DropItem(stackableItem)),
                    CancelAction = new ItemsCountChoiceAction("Отменить", () => CancelInteraction())
                });
            }
            else
            {
                inventoryManager.DropItem(selectedItemSlot.BaseItemState);
            }
        }

        public void CancelInteraction()
        {
            throw new NotImplementedException();
        }
    }
}