using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Game Configs/Craft/Recipes/Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private RecipesCategory _category;	    
        [SerializeField] private string _title;
        [TextArea(5, 20)]
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _requiredEnergyCount;
        [SerializeField] private IngredientSlotsTemplateView _ingredientCellsTemplate;
        [Space]
        [SerializeField] private RecipeVariant[] _recipeVariants;

        public Sprite Icon => _icon;
        public RecipesCategory Category => _category;
        public string Title => _title;
        public string Description => _description;
        public int RequiredEnergyCount => _requiredEnergyCount;
        public IngredientSlotsTemplateView IngredientCellsTemplate => _ingredientCellsTemplate;
        public RecipeVariant[] RecipeVariants => _recipeVariants;
    }
}