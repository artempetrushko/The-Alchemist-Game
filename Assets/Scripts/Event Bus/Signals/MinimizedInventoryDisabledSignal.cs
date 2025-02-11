using GameLogic.PlayerMenu.Inventory;

namespace EventBus
{
    public class MinimizedInventoryDisabledSignal
    {
        public readonly MinimizedInventoryView MinimizedInventoryView;

        public MinimizedInventoryDisabledSignal(MinimizedInventoryView minimizedInventoryView)
        {
            MinimizedInventoryView = minimizedInventoryView;
        }
    }
}