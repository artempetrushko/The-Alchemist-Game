using GameLogic.EnvironmentExploration;
using GameLogic.LootSystem;

namespace EventBus
{
    public class PickableItemPickedSignal : IItemPickedSignal
    {
        public readonly PickableItem PickableItem;

        public Item Item => PickableItem.ItemState;

        public PickableItemPickedSignal(PickableItem pickableItem)
        {
            PickableItem = pickableItem;
        }
    }
}