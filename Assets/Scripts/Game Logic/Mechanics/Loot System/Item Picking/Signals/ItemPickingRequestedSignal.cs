using GameLogic.LootSystem;

namespace GameLogic.EnvironmentExploration
{
    public class ItemPickingRequestedSignal
    {
        public readonly ItemState Item;

        public ItemPickingRequestedSignal(ItemState item)
        {
            Item = item;
        }
    }
}