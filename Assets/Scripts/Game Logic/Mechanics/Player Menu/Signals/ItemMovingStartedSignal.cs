namespace GameLogic.PlayerMenu
{
    public class ItemMovingStartedSignal
    {
        public readonly ItemSlot StartItemSlot;

        public ItemMovingStartedSignal(ItemSlot startItemSlot)
        {
            StartItemSlot = startItemSlot;
        }
    }
}