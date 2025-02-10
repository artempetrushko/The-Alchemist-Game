using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class ItemsSplittingModule : ItemsInteractionModule
    {
        [Space, SerializeField]
        private ItemsCountChoiceManager itemsCountChoiceManager;
        [SerializeField]
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

        public override void CancelInteraction()
        {
            throw new System.NotImplementedException();
        }
    }
}