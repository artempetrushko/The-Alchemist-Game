using TMPro;
using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
	public class ItemCraftingStatusPanelView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _energyCounterText;
		[SerializeField] private TMP_Text _craftingAvailabilityText;

		public void SetEnergyCounterText(string text) => _energyCounterText.text = text;

		public void SetCraftingAvailabilityText(string text) => _craftingAvailabilityText.text = text;

		public void SetCraftingAvailabilityTextColor(Color color) => _craftingAvailabilityText.color = color;
	}
}