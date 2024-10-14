namespace GameLogic.PlayerMenu
{
    public class BindQuickAccessSlotInteraction : ItemsInteraction, IInteractionExecutable, IInteractionCancelable
    {
        public override void Activate(ItemSlot selectedItemSlot)
        {
            //playerMenuSubsection.transform.SetAsLastSibling();
            //playerMenuSubsection.StartNavigation_WeaponEquipping();
            //weaponCellContainers[0].transform.SetAsLastSibling();
            //weaponCellContainers[1].transform.SetAsLastSibling();
        }

        public void Execute()
        {
            //playerSetSubsection.SelectedCell.GetComponent<ItemCellContainer>().SwapAndPlaceItem(currentItemCell);
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