using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
	public class IngredientSlotsTemplateView : MonoBehaviour
	{
		[SerializeField] private ItemSlotView[] _ingredientSlotViews;

        public ItemSlotView[] IngredientSlotViews => _ingredientSlotViews;

		public void SetActive(bool isActive) => gameObject.SetActive(isActive);
	}
}