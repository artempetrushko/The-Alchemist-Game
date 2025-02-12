using GameLogic.LootSystem;

namespace EventBus
{
    public abstract class ItemPickedSignal
    {
        public readonly Item Item;

        public ItemPickedSignal(Item item)
        {
            Item = item;
        }
    }
}