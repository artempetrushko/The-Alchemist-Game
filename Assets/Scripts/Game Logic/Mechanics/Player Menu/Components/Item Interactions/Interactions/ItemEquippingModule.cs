using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class ItemEquippingModule : ItemsInteractionModule
    {
        [SerializeField]
        private InventoryManager inventoryManager;

        public override void StartInteraction(ItemSlot selectedItemSlot)
        {
            inventoryManager.TryEquipInventoryItem(selectedItemSlot);
            OnInteractionExecuted();
        }

        public override void CancelInteraction()
        {
            throw new System.NotImplementedException();
        }
    }
}