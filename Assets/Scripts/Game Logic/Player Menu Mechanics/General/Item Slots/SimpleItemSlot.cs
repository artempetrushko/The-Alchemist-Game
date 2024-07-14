using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleItemSlot : ItemSlot<ItemState>
{
    public override List<ItemInteractionType> GetItemInteractions()
    {
        var itemInteractions = new List<ItemInteractionType>()
        {
            ItemInteractionType.BindQuickAccess,
            ItemInteractionType.Drop
        };
        if (ItemState is EquipmentState)
        {
            itemInteractions.Insert(0, ItemInteractionType.Equip);
        }
        else if (ItemState is StackableItemState)
        {
            itemInteractions.InsertRange(0, new List<ItemInteractionType>()
            {
                ItemInteractionType.Join,
                ItemInteractionType.Split,
            });
        }
        return itemInteractions;
    }

    protected override bool TryPlaceOrSwapItem<P>(ItemSlot<P> previousInventorySlot)
    {
        if (BaseItemState != null)
        {
            if (previousInventorySlot.BaseItemState is StackableItemState draggingStackableItem)
            {
                if (previousInventorySlot.BaseItemState.BaseParams.ID != BaseItemState.BaseParams.ID)
                {
                    SwapItems(previousInventorySlot);
                    return true;
                }
                var containedStackableItem = BaseItemState as StackableItemState;
                if (containedStackableItem.ItemsCount < containedStackableItem.MaxStackItemsCount)
                {
                    containedStackableItem.ItemsCount += draggingStackableItem.ItemsCount;
                    if (containedStackableItem.ItemsCount <= containedStackableItem.MaxStackItemsCount)
                    {
                        previousInventorySlot.ClearItemState();
                    }
                    else
                    {
                        draggingStackableItem.ItemsCount = containedStackableItem.ItemsCount - containedStackableItem.MaxStackItemsCount;
                        containedStackableItem.ItemsCount = containedStackableItem.MaxStackItemsCount;
                    }
                    return true;
                }
            }
            else
            {
                if ((previousInventorySlot is WeaponItemSlot && ItemState is WeaponState)
                    || previousInventorySlot is ClothesItemSlot clothesItemData && ItemState is ClothesState currentClothes && (currentClothes.BaseParams as ClothesData).ClothesType == clothesItemData.ClothesType)
                {
                    SwapItems(previousInventorySlot);
                    return true;
                }
            }
        }
        else
        {
            PlaceItem(previousInventorySlot);
            return true;
        }
        return false;
    }
}
