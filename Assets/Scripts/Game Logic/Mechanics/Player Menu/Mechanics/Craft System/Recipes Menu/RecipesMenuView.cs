using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
    public class RecipesMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _recipesCategoriesContainer;

        public GameObject RecipesCategoriesContainer => _recipesCategoriesContainer;
    }
}