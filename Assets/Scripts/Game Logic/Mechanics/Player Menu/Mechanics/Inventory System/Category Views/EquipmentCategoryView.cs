using System.Linq;
using UnityEngine;

namespace GameLogic.PlayerMenu.Inventory
{
    public class EquipmentCategoryView : InventoryCategoryView
    {
        [SerializeField] private ItemSlotView _leftWeaponSlotView;
        [SerializeField] private ItemSlotView _rightWeaponSlotView;
        [SerializeField] private ItemSlotView _hatSlotView;
        [SerializeField] private ItemSlotView _raincoatSlotView;
        [SerializeField] private ItemSlotView _bootsSlotView;
        [SerializeField] private ItemSlotView _glovesSlotView;
        [SerializeField] private ItemSlotView _medallionSlotView;
        [SerializeField] private ItemSlotView[] _quickAccessSlotViews;

        public ItemSlotView LeftWeaponSlotView => _leftWeaponSlotView;
        public ItemSlotView RightWeaponSlotView => _rightWeaponSlotView;
        public ItemSlotView HatSlotView => _hatSlotView;
        public ItemSlotView RaincoatSlotView => _raincoatSlotView;
        public ItemSlotView BootsSlotView => _bootsSlotView;
        public ItemSlotView GlovesSlotView => _glovesSlotView;
        public ItemSlotView MedallionSlotView => _medallionSlotView;
        public ItemSlotView[] QuickAccessSlotViews => _quickAccessSlotViews;
    }
}