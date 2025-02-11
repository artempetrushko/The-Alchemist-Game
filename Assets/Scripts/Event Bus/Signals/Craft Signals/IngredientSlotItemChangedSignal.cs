using GameLogic.PlayerMenu.Craft;

namespace EventBus
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