using GameLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClothesItemSlot : ItemSlot<ClothesState>
{
    [SerializeField]
    private ClothesType clothesType;

    public ClothesType ClothesType => clothesType;

    protected override bool TryPlaceOrSwapItem<P>(ItemSlot<P> previousInventorySlot)
    {
        if (((previousInventorySlot.ItemState as ClothesState).BaseParams as ClothesData).ClothesType == ClothesType)
        {
            PlaceOrSwapItem(previousInventorySlot);
            return true;
        }
        return false;
    }

    public override List<ItemInteractionType> GetItemInteractions()
    {
        return new()
        {
            ItemInteractionType.TakeOff,
            ItemInteractionType.Drop
        };
    }
}
