using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAccessItemSlot : SimpleItemSlot
{
    public override List<ItemInteractionType> GetItemInteractions()
    {
        var itemInteractions = new List<ItemInteractionType>() 
        { 
            ItemInteractionType.TakeOff,
            ItemInteractionType.BindQuickAccess,
            ItemInteractionType.Drop
        };
        if (ItemState is EquipmentState)
        {
            itemInteractions.Insert(1, ItemInteractionType.Equip);
        }
        else if (ItemState is StackableItemState)
        {
            itemInteractions.InsertRange(1, new List<ItemInteractionType>()
            {
                ItemInteractionType.Join,
                ItemInteractionType.Split
            });
        }
        return itemInteractions;
    }
}
