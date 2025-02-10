using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            itemsCountChoiceManager.CreateItemsCountChoiceView("�������� ���������� ���������", stackableItem, 1, stackableItem.ItemsCount, new ItemsCountChoiceData()
            {
                ConfirmAction = new ItemsCountChoiceAction("���������", () => inventoryManager.DropItem(stackableItem, itemsCountChoiceManager.SelectedItemsCount)),
                ConfirmAllAction = new ItemsCountChoiceAction("��������� ��", () => inventoryManager.DropItem(stackableItem)),
                CancelAction = new ItemsCountChoiceAction("��������", () => CancelInteraction())
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
