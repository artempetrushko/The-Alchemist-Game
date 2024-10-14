using UnityEngine;

namespace GameLogic.PlayerMenu.Inventory
{
    public class InventoryCategoryButtonData
    {
        [SerializeField] private PlayerMenuSectionSelectButton _button;
        [SerializeField] private InventoryCategoryView _linkedCategoryView;

        public PlayerMenuSectionSelectButton Button => _button;
        public InventoryCategoryView LinkedCategoryView => _linkedCategoryView;
    }
}