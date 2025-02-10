using GameLogic.LootSystem;

namespace GameLogic.EnvironmentExploration
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