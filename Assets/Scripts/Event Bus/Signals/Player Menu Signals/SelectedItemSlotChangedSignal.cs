using GameLogic.PlayerMenu;

namespace EventBus
{
    public class SelectedItemSlotChangedSignal
    {
        public readonly ItemSlot SelectedItemSlot;

        public SelectedItemSlotChangedSignal(ItemSlot selectedItemSlot)
        {
            SelectedItemSlot = selectedItemSlot;
        }
    }
}