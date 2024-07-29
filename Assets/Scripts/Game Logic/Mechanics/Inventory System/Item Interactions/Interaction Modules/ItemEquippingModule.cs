namespace GameLogic.Inventory
{
    public class ItemEquippingModule : ItemsInteractionModule
    {
        private InventoryManager inventoryManager;

        public override void StartInteraction(ItemSlot selectedItemSlot)
        {
            inventoryManager.TryEquipInventoryItem(selectedItemSlot);
            OnInteractionExecuted();
        }
    }
}