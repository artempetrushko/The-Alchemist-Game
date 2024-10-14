namespace GameLogic.PlayerMenu.Craft
{
    public class CraftModel
    {
        public IngredientSlots IngredientSlots;
        public EnergySlots EnergySlots;

        public (Recipe recipe, IngredientSlotsTemplateView ingredientSlotsTemplate)[] RecipeInfos;
        public Recipe CurrentRecipe;
        public RecipeVariant CurrentRecipeVariant;
        public CraftingAvailabilityStatus CraftingAvailabilityStatus;
        public int CurrentExtractedEnergyCount;
        public bool IsAllIngredientsPlaced;
    }
}