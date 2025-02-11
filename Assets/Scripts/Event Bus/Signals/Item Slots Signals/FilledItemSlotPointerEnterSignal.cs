using GameLogic.PlayerMenu;

namespace EventBus
{
    public class FilledItemSlotPointerEnterSignal
    {
        public readonly ItemSlot ItemSlot;

        public FilledItemSlotPointerEnterSignal(ItemSlot itemSlot)
        {
            ItemSlot = itemSlot;
        }
    }
}