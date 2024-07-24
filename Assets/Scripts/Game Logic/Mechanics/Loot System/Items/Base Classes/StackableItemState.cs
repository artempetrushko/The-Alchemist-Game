using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableItemState : ItemState
{
    public int ItemsCount { get; set; }
    public int MaxStackItemsCount { get; set; }
    public int TotalContainedEnergyCount => ContainedEnergyCount * ItemsCount;

    public StackableItemState(StackableItemData item, int itemsCount = 0) : base(item)
    {
        ItemsCount = itemsCount > 0 ? itemsCount : item.BaseCount;
        MaxStackItemsCount = item.StackMaxCount;
    }
}
