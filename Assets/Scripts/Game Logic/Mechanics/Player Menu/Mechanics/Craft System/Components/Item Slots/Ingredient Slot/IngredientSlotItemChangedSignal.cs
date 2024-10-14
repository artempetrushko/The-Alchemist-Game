namespace GameLogic.PlayerMenu.Craft
{
    public class IngredientSlotItemChangedSignal
    {
        public readonly IngredientSlot ItemSlot;

        public IngredientSlotItemChangedSignal(IngredientSlot itemSlot)
        {
            ItemSlot = itemSlot;
        }
    }
}