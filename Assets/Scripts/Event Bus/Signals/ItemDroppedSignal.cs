using GameLogic.LootSystem;

namespace EventBus
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