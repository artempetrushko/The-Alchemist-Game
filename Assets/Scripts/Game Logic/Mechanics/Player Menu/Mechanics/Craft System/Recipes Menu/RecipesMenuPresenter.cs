using System;
using System.Linq;
using Object = UnityEngine.Object;

namespace GameLogic.PlayerMenu.Craft
{
    public class RecipesMenuPresenter
    {
        public event Action<Recipe> RecipeSelected;

        private Recipe _currentRecipe;
        private RecipeVariant _selectedRecipeVariant;

        private RecipesMenuView _recipesSectionView;
        private RecipeCategoryCardView _recipesCategoryViewPrefab;
        private RecipeCardView _recipeButtonPrefab;

        public RecipeCategoryCardView[] RecipeCategories => _recipesSectionView.RecipesCategoriesContainer.GetComponentsInChildren<RecipeCategoryCardView>();

        public RecipesMenuPresenter(RecipesMenuView recipesSectionView)
        {
            _recipesSectionView = recipesSectionView;
        }

        public void CreateRecipeViews(RecipesConfig recipesConfig)
        {

        }









        






        /*public bool IsOpened => _recipesSectionView.RecipesCategoriesContainer.activeInHierarchy;
        public RecipeButton[] RecipeButtons => _recipeButtonsContainer.GetComponentsInChildren<RecipeButton>();

        public void SetInfo(string categoryTitle, RecipeData[] recipes, Action<RecipeData> recipeButtonsAction)
        {
            _title.text = categoryTitle;
            _buttonComponent.onClick.AddListener(() => _recipeButtonsContainer.SetActive(!_recipeButtonsContainer.activeInHierarchy));
            foreach (var recipe in recipes)
            {
                var recipeButton = Instantiate(recipeButtonPrefab, _recipeButtonsContainer.transform);
                recipeButton.SetTitleText(recipe.Title);
                recipeButton.SetRecipeDescriptionText(recipe.Description);
                recipeButton.SetRecipeResultItemIcon(recipe.ResultItemIcon);
                recipeButton.ButtonComponent.onClick.AddListener(() => recipeButtonsAction(recipe));
            }
        }

        public void SetRecipeButtonsActive(bool isActive) => _recipeButtonsContainer.SetActive(isActive);*/
    }
}