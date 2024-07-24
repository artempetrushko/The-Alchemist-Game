using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSplittingModule : ItemsInteractionModule, IInteractionCancelable
{
    [Space, SerializeField]
    private ItemsCountSelectPanelController itemsCountChoiceManager;
    [SerializeField]
    private InventoryManager inventoryManager;

    public override void StartInteraction(ItemSlot selectedItemSlot)
    {
        if (selectedItemSlot.BaseItemState is StackableItemState stackableItem)
        {
            itemsCountChoiceManager.CreateItemsCountChoiceView("�������� ���������� ���������", stackableItem, 1, stackableItem.ItemsCount - 1, new ItemsCountChoiceData()
            {
                ConfirmAction = new ItemsCountChoiceAction("��������", () =>
                {
                    if (inventoryManager.TryAddStackableItemCopy(stackableItem, itemsCountChoiceManager.SelectedItemsCount))
                    {
                        stackableItem.ItemsCount -= itemsCountChoiceManager.SelectedItemsCount;
                    }
                }),
                CancelAction = new ItemsCountChoiceAction("��������", () => CancelInteraction())
            });
        }
    }

    public void CancelInteraction()
    {
        throw new System.NotImplementedException();
    }
}
