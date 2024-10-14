using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.PlayerMenu.Craft
{
    [CreateAssetMenu(fileName = "Item Crafting Status Panel Config", menuName = "Game Configs/Craft/Item Crafting Status Panel Config")]
	public class ItemCraftingStatusPanelConfig : ScriptableObject
	{
		[SerializeField] private LocalizedString _energyCounterLabelText;
		[Space]
		[SerializeField] private CraftingAvailabilityStatusTextsConfig[] _craftingAvailabilityStatusTextsConfigs;
		[SerializeField] private Color _craftingAvailabilityColor;
		[SerializeField] private Color _craftingUnavailabilityColor;

		public LocalizedString EnergyCounterLabelText => _energyCounterLabelText;
		public CraftingAvailabilityStatusTextsConfig[] CraftingAvailabilityStatusTextsConfigs => _craftingAvailabilityStatusTextsConfigs;
		public Color CraftingAvailabilityColor => _craftingAvailabilityColor;
		public Color CraftingUnavailabilityColor => _craftingUnavailabilityColor;
	}
}