namespace GameLogic.PlayerMenu
{
    public class ItemMovingFinishedSignal
    {
        public readonly ItemSlot DestinationItemSlot;

        public ItemMovingFinishedSignal(ItemSlot destinationItemSlot)
        {
            DestinationItemSlot = destinationItemSlot;
        }
    }
}