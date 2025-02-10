using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu.Craft
{
    public class ItemCraftedSignal
    {
        public readonly Item Item;

        public ItemCraftedSignal(Item item)
        {
            Item = item;
        }
    }
}