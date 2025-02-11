using GameLogic.LootSystem;

namespace EventBus
{
    public class ItemPickingRequestedSignal
    {
        public readonly Item Item;

        public ItemPickingRequestedSignal(Item item)
        {
            Item = item;
        }
    }
}