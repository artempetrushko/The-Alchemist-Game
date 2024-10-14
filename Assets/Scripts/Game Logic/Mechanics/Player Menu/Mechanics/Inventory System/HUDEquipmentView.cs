using UnityEngine;

namespace GameLogic.PlayerMenu.Inventory
{
	public class HUDEquipmentView : MonoBehaviour
    {
		[SerializeField] private ItemSlotView _leftWeaponSlotView;
        [SerializeField] private ItemSlotView _rightWeaponSlotView;
		[SerializeField] private GameObject _quickAccessSlotViewsContainer;

		public ItemSlotView LeftWeaponSlotView => _leftWeaponSlotView;
		public ItemSlotView RightWeaponSlotView => _rightWeaponSlotView;		
		public ItemSlotView[] QuickAccessSlotViews => _quickAccessSlotViewsContainer.GetComponentsInChildren<ItemSlotView>();
	}
}