namespace GameLogic.PlayerMenu.Inventory
{
    public class EquipItemInteraction : ItemsInteraction
    {
        private InventoryPresenter inventoryManager;

        public override bool CheckAvailability()
        {
            throw new System.NotImplementedException();
        }

        public override void Activate(ItemSlot selectedItemSlot)
        {
            inventoryManager.TryAddItem(selectedItemSlot.ContainedItem);
        }
    }
}