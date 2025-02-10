namespace GameLogic.PlayerMenu.Inventory
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