using System;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class ItemsDroppingModule : ItemsInteractionModule
    {
        [Space, SerializeField]
        private ItemsCountChoiceManager itemsCountChoiceManager;
        [SerializeField]
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

        public override void CancelInteraction()
        {
            throw new NotImplementedException();
        }
    }
}