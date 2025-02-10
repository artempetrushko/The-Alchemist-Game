using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.PlayerMenu.Craft
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Game Configs/Craft/Recipes/Recipe")]
    public class Recipe : ScriptableObject
    {   
        [SerializeField] private LocalizedString _title;
        [SerializeField] private LocalizedString _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _requiredEnergyCount;
        [SerializeField] private IngredientSlotsTemplateView _ingredientCellsTemplate;
        [Space]
        [SerializeField] private RecipeVariant[] _recipeVariants;

        public Sprite Icon => _icon;
        public LocalizedString Title => _title;
        public LocalizedString Description => _description;
        public int RequiredEnergyCount => _requiredEnergyCount;
        public IngredientSlotsTemplateView IngredientCellsTemplate => _ingredientCellsTemplate;
        public RecipeVariant[] RecipeVariants => _recipeVariants;
    }
}