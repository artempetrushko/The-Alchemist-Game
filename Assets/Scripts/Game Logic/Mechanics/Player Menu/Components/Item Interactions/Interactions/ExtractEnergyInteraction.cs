namespace GameLogic.PlayerMenu
{
    public class ExtractEnergyInteraction : ItemsInteraction, IInteractionExecutable, IInteractionCancelable
    {
        public override void Activate(ItemSlot selectedItemSlot)
        {
            StartItemSlot = selectedItemSlot;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
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