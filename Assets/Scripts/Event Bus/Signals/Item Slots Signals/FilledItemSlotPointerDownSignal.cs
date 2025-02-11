using GameLogic.PlayerMenu;

namespace EventBus
{
    public class FilledItemSlotPointerDownSignal
    {
        public readonly ItemSlot ItemSlot;

        public FilledItemSlotPointerDownSignal(ItemSlot itemSlot)
        {
            ItemSlot = itemSlot;
        }
    }
}