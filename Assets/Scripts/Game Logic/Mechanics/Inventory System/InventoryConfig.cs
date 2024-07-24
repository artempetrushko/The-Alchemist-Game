using UnityEngine;

namespace GameLogic.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Config", menuName = "Game Data/Inventory/Inventory Config")]
    public class InventoryConfig : ScriptableObject
    {
        [SerializeField] private int _inventorySlotsCount;
        [SerializeField] private int _quickAccessSlotsCount;

        public int InventorySlotsCount => _inventorySlotsCount;
        public int QuickAccessSlotsCount => _quickAccessSlotsCount;
    }
}
