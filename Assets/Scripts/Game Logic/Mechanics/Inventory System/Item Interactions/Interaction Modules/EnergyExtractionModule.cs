using UI.PlayerMenu;

namespace GameLogic.Inventory
{
    public class EnergyExtractionModule : ItemsInteractionModule, IInteractionExecutable, IInteractionCancelable
    {
        private PlayerMenuController playerMenuManager;

        public override void StartInteraction(ItemSlot selectedItemSlot)
        {
            startItemSlot = selectedItemSlot;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void CancelInteraction()
        {
            throw new System.NotImplementedException();
        }
    }
}