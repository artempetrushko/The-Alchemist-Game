namespace GameLogic.PlayerMenu
{
    public class JoinItemsInteraction : ItemsInteraction, IInteractionExecutable, IInteractionCancelable
    {
        public override void Activate(ItemSlot selectedItemSlot)
        {
            StartItemSlot = selectedItemSlot;
        }

        public void Execute()
        {
            
        }

        public void CancelInteraction()
        {
            throw new System.NotImplementedException();
        }

        public override bool CheckAvailability()
        {
            throw new System.NotImplementedException();
        }
    }
}