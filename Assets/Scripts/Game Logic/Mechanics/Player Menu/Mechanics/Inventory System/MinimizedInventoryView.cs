using UnityEngine;

namespace GameLogic.PlayerMenu.Inventory
{
    public class MinimizedInventoryView : InventoryView
    {
        [SerializeField] private InventoryCategoryButtonData[] _inventoryCategoryButtonDatas;

        public InventoryCategoryButtonData[] InventoryCategoryButtonDatas => _inventoryCategoryButtonDatas;
    }
}