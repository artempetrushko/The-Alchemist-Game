using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu
{
    public class MainInventorySlots : ItemSlotCollection
    {
        public ItemSlot[] Slots { get; private set; }

        public MainInventorySlots(int slotsCount)
        {

        }

        public bool TryPlaceItem(Item item)
        {
            return false;
        }
    }
}