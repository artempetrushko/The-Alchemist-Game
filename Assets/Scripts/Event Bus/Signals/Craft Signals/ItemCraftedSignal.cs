using GameLogic.LootSystem;

namespace EventBus
{
    public class ItemCraftedSignal
    {
        public readonly Item Item;

        public ItemCraftedSignal(Item item)
        {
            Item = item;
        }
    }
}