using GameLogic.PlayerMenu;

namespace EventBus
{
    public class FilledItemSlotPointerExitSignal
    {
        public readonly ItemSlot ItemSlot;

        public FilledItemSlotPointerExitSignal(ItemSlot itemSlot)
        {
            ItemSlot = itemSlot;
        }
    }
}