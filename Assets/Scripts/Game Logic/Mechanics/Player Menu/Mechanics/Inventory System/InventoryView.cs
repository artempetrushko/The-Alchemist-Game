using UnityEngine;

namespace GameLogic.PlayerMenu.Inventory
{
    public class InventoryView : MonoBehaviour
	{
		[SerializeField] private MainInventoryCategoryView _mainInventoryCategoryView;
		[SerializeField] private EquipmentCategoryView _equipmentCategoryView;

		public MainInventoryCategoryView MainInventoryCategoryView => _mainInventoryCategoryView;
		public EquipmentCategoryView EquipmentCategoryView => _equipmentCategoryView;

		public void SetActive(bool isActive) => gameObject.SetActive(isActive);
	}
}