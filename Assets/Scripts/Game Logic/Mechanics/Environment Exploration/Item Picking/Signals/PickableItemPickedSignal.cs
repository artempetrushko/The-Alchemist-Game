using GameLogic.LootSystem;

namespace GameLogic.EnvironmentExploration
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