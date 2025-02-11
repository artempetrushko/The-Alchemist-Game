using GameLogic.PlayerMenu;

namespace EventBus
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