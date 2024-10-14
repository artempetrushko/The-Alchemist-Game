using GameLogic.EnvironmentExploration;

namespace GameLogic.Player
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