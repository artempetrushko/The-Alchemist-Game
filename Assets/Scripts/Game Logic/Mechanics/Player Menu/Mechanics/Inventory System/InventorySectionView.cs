using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class InventorySectionView : PlayerMenuSectionView
    {
        [SerializeField]
        private InventorySubsectionView inventorySubsectionView;

        public InventorySubsectionView InventorySubsectionView => inventorySubsectionView;
    }
}