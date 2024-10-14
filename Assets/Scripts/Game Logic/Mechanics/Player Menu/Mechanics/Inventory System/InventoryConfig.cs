using UnityEngine;

namespace GameLogic.PlayerMenu.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Config", menuName = "Game Configs/Inventory/Inventory Config")]
    public class InventoryConfig : ScriptableObject
    {
        [SerializeField] private int _inventorySlotsCount;
        [SerializeField] private int _quickAccessSlotsCount;
        [Space]
        [SerializeField] private ItemSlotInteractionsConfig _mainInventorySlotsInteractions;
        [SerializeField] private ItemSlotInteractionsConfig _quickAccessSlotsInteractions;
        [SerializeField] private ItemSlotInteractionsConfig _weaponSlotsInteractions;
        [SerializeField] private ItemSlotInteractionsConfig _clothesSlotsInteractions;

        public int InventorySlotsCount => _inventorySlotsCount;
        public int QuickAccessSlotsCount => _quickAccessSlotsCount;
        public ItemSlotInteractionsConfig MainInventorySlotsInteractions => _mainInventorySlotsInteractions;
        public ItemSlotInteractionsConfig QuickAccessSlotsInteractions => _quickAccessSlotsInteractions;
        public ItemSlotInteractionsConfig WeaponSlotsInteractions => _weaponSlotsInteractions;
        public ItemSlotInteractionsConfig ClothesSlotsSInteractions => _clothesSlotsInteractions;
    }
}
