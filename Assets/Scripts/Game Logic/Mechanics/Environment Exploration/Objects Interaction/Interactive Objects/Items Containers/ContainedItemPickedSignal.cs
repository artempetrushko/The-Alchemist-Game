using GameLogic.LootSystem;

namespace GameLogic.EnvironmentExploration
{
    public class ContainedItemPickedSignal : IItemPickedSignal
    {
        private readonly Item _item;

        public Item Item => _item;

        public ContainedItemPickedSignal(Item item)
        {
            _item = item;
        }
    }
}