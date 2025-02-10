using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class ItemsConcatinationModule : ItemsInteractionModule, IInteractionExecutable
    {
        [SerializeField]
        private PlayerMenuManager playerMenuManager;

        public override void StartInteraction(ItemSlot selectedItemSlot)
        {
            startItemSlot = selectedItemSlot;
        }

        public override void CancelInteraction()
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            var currentSubsection = playerMenuManager.CurrentSectionView.SectionNavigation.CurrentSubsection;
            if (currentSubsection is CellsSubsectionNavigation cellsSubsection)
            {
                var concatinatingItemSlot = cellsSubsection.SelectedCell.LinkedItemSlot;
                if (concatinatingItemSlot.BaseItemState.BaseParams.Equals(startItemSlot.BaseItemState.BaseParams))
                {
                    concatinatingItemSlot.TryPlaceOrSwapItem(startItemSlot);
                    OnInteractionExecuted();
                }
            }
        }
    }
}