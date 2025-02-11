using GameLogic.PlayerMenu.Inventory;

namespace EventBus
{
    public class MinimizedInventoryEnabledSignal
    {
        public readonly MinimizedInventoryView MinimizedInventoryView;

        public MinimizedInventoryEnabledSignal(MinimizedInventoryView minimizedInventoryView)
        {
            MinimizedInventoryView = minimizedInventoryView;
        }
    }
}