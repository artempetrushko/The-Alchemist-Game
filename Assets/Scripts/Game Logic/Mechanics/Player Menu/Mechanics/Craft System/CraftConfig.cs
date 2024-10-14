using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
    [CreateAssetMenu(fileName = "Craft Config", menuName = "Game Configs/Craft/Craft Config")]
    public class CraftConfig : ScriptableObject
    {
        [SerializeField] private Recipe[] _availableCraftRecipes;
        [SerializeField] private CraftingAvailabilityStatusTextsConfig[] _craftingAvailabilityStatusTextsConfigs;
        [SerializeField] private float _craftingTimeInSeconds;
        [SerializeField] private float _craftingStepTimeInSeconds;

        public Recipe[] AvailableCraftRecipes => _availableCraftRecipes;
        public CraftingAvailabilityStatusTextsConfig[] CraftingAvailabilityStatusTextsConfigs => _craftingAvailabilityStatusTextsConfigs;
        public float CraftingTimeInSeconds => _craftingTimeInSeconds;
        public float CraftingStepTimeInSeconds => _craftingStepTimeInSeconds;
    }
}
