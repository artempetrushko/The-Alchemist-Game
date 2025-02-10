using System;
using System.Collections.Generic;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
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

        public override List<ItemInteraction> GetItemInteractions()
        {
            return new()
        {
            ItemInteraction.TakeOff,
            ItemInteraction.Drop
        };
        }
    }
}