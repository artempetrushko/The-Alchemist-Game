using System.Collections.Generic;
using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu
{
    public class QuickAccessItemSlot : SimpleItemSlot
    {
        public override List<ItemInteraction> GetItemInteractions()
        {
            var itemInteractions = new List<ItemInteraction>()
        {
            ItemInteraction.TakeOff,
            ItemInteraction.BindQuickAccess,
            ItemInteraction.Drop
        };
            if (ItemState is EquipmentState)
            {
                itemInteractions.Insert(1, ItemInteraction.Equip);
            }
            else if (ItemState is StackableItemState)
            {
                itemInteractions.InsertRange(1, new List<ItemInteraction>()
            {
                ItemInteraction.Join,
                ItemInteraction.Split
            });
            }
            return itemInteractions;
        }
    }
}