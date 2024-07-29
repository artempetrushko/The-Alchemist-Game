namespace GameLogic.Inventory
{
    public class ItemsSplittingModule : ItemsInteractionModule, IInteractionCancelable
    {
        private ItemsCountSelectPanelController itemsCountChoiceManager;
        private InventoryManager inventoryManager;

        public override void StartInteraction(ItemSlot selectedItemSlot)
        {
            if (selectedItemSlot.BaseItemState is StackableItemState stackableItem)
            {
                itemsCountChoiceManager.CreateItemsCountChoiceView("Выберите количество предметов", stackableItem, 1, stackableItem.ItemsCount - 1, new ItemsCountChoiceData()
                {
                    ConfirmAction = new ItemsCountChoiceAction("Отделить", () =>
                    {
                        if (inventoryManager.TryAddStackableItemCopy(stackableItem, itemsCountChoiceManager.SelectedItemsCount))
                        {
                            stackableItem.ItemsCount -= itemsCountChoiceManager.SelectedItemsCount;
                        }
                    }),
                    CancelAction = new ItemsCountChoiceAction("Отменить", () => CancelInteraction())
                });
            }
        }

        public void CancelInteraction()
        {
            throw new System.NotImplementedException();
        }
    }
}