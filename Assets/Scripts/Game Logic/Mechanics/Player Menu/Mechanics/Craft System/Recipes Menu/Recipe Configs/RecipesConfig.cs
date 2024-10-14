using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
    [CreateAssetMenu(fileName = "Recipes Config", menuName = "Game Configs/Craft/Recipes/Recipes Config")]
    public class RecipesConfig : ScriptableObject
    {
        [SerializeField] private RecipesCategory[] _recipesCategories;

        public RecipesCategory[] RecipesCategories => _recipesCategories;
    }
}