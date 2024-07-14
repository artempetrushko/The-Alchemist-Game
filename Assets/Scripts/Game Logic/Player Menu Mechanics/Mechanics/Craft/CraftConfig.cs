using UnityEngine;

namespace GameLogic.Craft
{
    [CreateAssetMenu(fileName = "Craft Config", menuName = "Game Data/Craft/Craft Config")]
    public class CraftConfig : ScriptableObject
    {
        [SerializeField] private RecipeData[] _availableCraftRecipes;
        [SerializeField] private float _craftingTimeInSeconds;
        [SerializeField] private float _craftingStepTimeInSeconds;

        public RecipeData[] AvailableCraftRecipes => _availableCraftRecipes;
        public float CraftingTimeInSeconds => _craftingTimeInSeconds;
        public float CraftingStepTimeInSeconds => _craftingStepTimeInSeconds;
    }
}
