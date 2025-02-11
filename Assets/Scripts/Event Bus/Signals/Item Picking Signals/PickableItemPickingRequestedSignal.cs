using GameLogic.EnvironmentExploration;

namespace EventBus
{
    public class PickableItemPickingRequestedSignal
    {
        public readonly PickableItem PickableItem;

        public PickableItemPickingRequestedSignal(PickableItem pickableItem)
        {
            PickableItem = pickableItem;
        }
    }
}