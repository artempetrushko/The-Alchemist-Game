using UnityEngine;

namespace UI.PlayerMenu
{
    public class OrderedCellsSubsectionNavigation : CellsSubsectionNavigation
    {
        [Space, SerializeField]
        private ItemCellNavigationModule leftStartItemCell;
        [SerializeField]
        private ItemCellNavigationModule rightStartItemCell;
        [SerializeField]
        private ItemCellNavigationModule topStartItemCell;
        [SerializeField]
        private ItemCellNavigationModule bottomStartItemCell;

        private ItemCellNavigationModule SelectedOrderedItemCell
        {
            get => SelectedCell.GetComponent<ItemCellNavigationModule>();
            set => SelectedCell = value.GetComponent<ItemCellView>();
        }

        public override void StartNavigation(SubsectionNavigationStartCondition condition = SubsectionNavigationStartCondition.Default)
        {
            SelectedOrderedItemCell = condition switch
            {
                SubsectionNavigationStartCondition.TransitionFromLeftSubsection => leftStartItemCell,
                SubsectionNavigationStartCondition.TransitionFromRightSubsection => rightStartItemCell,
                SubsectionNavigationStartCondition.TransitionFromTopSubsection => topStartItemCell,
                SubsectionNavigationStartCondition.TransitionFromBottomSubsection => bottomStartItemCell,
                SubsectionNavigationStartCondition.Default => topStartItemCell
            };
        }

        public override void Navigate(Vector2 inputValue)
        {
            switch (inputValue.x)
            {
                case 1:
                    SelectNewItemCellOrSubsection(SelectedOrderedItemCell.RightNeighboringCell, (rightNeighboringSubsection, SubsectionNavigationStartCondition.TransitionFromLeftSubsection));
                    break;

                case -1:
                    SelectNewItemCellOrSubsection(SelectedOrderedItemCell.LeftNeighboringCell, (leftNeighboringSubsection, SubsectionNavigationStartCondition.TransitionFromRightSubsection));
                    break;
            }
            switch (inputValue.y)
            {
                case 1:
                    SelectNewItemCellOrSubsection(SelectedOrderedItemCell.TopNeighboringCell, (topNeighboringSubsection, SubsectionNavigationStartCondition.TransitionFromBottomSubsection));
                    break;

                case -1:
                    SelectNewItemCellOrSubsection(SelectedOrderedItemCell.BottomNeighboringCell, (bottomNeighboringSubsection, SubsectionNavigationStartCondition.TransitionFromTopSubsection));
                    break;
            }
        }

        private void SelectNewItemCellOrSubsection(ItemCellNavigationModule newItemCell, (PlayerMenuSubsectionNavigation newSubsection, SubsectionNavigationStartCondition transitionCondition) subsectionChangingData)
        {
            if (newItemCell != null)
            {
                SelectedOrderedItemCell = newItemCell;
            }
            else if (subsectionChangingData.newSubsection != null)
            {
                parentSection.SetCurrentSubsection(subsectionChangingData.newSubsection, subsectionChangingData.transitionCondition);
            }
        }
    }
}