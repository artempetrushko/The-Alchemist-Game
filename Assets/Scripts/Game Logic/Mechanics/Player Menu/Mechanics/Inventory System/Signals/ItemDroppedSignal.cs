using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu.Inventory
{
    public class ItemDroppedSignal
    {
        public readonly Item Item;

        public ItemDroppedSignal(Item item)
        {
            Item = item;
        }
    }
}