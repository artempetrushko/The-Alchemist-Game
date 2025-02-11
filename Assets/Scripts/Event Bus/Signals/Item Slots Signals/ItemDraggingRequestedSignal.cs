using GameLogic.PlayerMenu;

namespace EventBus
{
    public class ItemDraggingRequestedSignal
    {
        public readonly ItemSlot SelectedSlot;

        public ItemDraggingRequestedSignal(ItemSlot selectedSlot)
        {
            SelectedSlot = selectedSlot;
        }
    }
}