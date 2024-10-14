namespace GameLogic.PlayerMenu.Inventory
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