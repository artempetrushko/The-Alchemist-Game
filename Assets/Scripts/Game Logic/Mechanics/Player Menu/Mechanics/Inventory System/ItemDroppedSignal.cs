using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu.Inventory
{
    public class ItemDroppedSignal
    {
        public readonly ItemState Item;

        public ItemDroppedSignal(ItemState item)
        {
            Item = item;
        }
    }
}