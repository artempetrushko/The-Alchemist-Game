using GameLogic.EnvironmentExploration;
using GameLogic.LootSystem;

namespace GameLogic.Player
{
    public class PickableItemPickedSignal : IItemPickedSignal
    {
        public readonly PickableItem PickableItem;

        public ItemState Item => PickableItem.ItemState;

        public PickableItemPickedSignal(PickableItem pickableItem)
        {
            PickableItem = pickableItem;
        }
    }
}