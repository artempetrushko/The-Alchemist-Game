using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu.Craft
{
    public class ItemCraftedSignal
    {
        public readonly ItemState Item;

        public ItemCraftedSignal(ItemState item)
        {
            Item = item;
        }
    }
}