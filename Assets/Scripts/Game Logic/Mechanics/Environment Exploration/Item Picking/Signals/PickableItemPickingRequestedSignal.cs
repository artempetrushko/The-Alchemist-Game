namespace GameLogic.EnvironmentExploration
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