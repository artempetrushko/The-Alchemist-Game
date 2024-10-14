using GameLogic.LootSystem;

namespace GameLogic.EnvironmentExploration
{
    public class ContainedItemPickedSignal : IItemPickedSignal
    {
        private readonly ItemState _item;

        public ItemState Item => _item;

        public ContainedItemPickedSignal(ItemState item)
        {
            _item = item;
        }
    }
}