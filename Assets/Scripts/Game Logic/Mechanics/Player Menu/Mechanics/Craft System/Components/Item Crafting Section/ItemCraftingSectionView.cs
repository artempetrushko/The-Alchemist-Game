using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu.Craft
{
    public class ItemCraftingSectionView : MonoBehaviour
	{
		[SerializeField] private ItemCraftingPlaceView _itemCraftingPlaceView;
        [SerializeField] private Image _craftProgressBar;
        [SerializeField] private ItemSlotView[] _energySlotViews;    

		public ItemCraftingPlaceView ItemCraftingPlaceView => _itemCraftingPlaceView;
		public ItemSlotView[] EnergySlotViews => _energySlotViews;

		public void SetCraftProgressBarActive(bool isActive) => _craftProgressBar.gameObject.SetActive(isActive);

		public void SetCraftProgressBarFillAmount(float fillAmount) => _craftProgressBar.fillAmount = fillAmount;
	}
}