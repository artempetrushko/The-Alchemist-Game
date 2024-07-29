using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerMenu
{
    public class InventoryNavigation : CellsSubsectionNavigation
    {
        private GridLayoutGroup gridLayout;

        private int CellsGridConstraintCount => gridLayout.constraintCount;

        public override void Navigate(Vector2 inputValue)
        {
            if (Mathf.Abs(inputValue.x) == 1)
            {
                if (inputValue.x == -1 && (SelectedCellNumber - 1) % CellsGridConstraintCount == 0 && !InventoryItemMovingManager.IsMovingStarted)
                {
                    parentSection.SetCurrentSubsection(leftNeighboringSubsection, SubsectionNavigationStartCondition.TransitionFromRightSubsection);
                }
                else
                {
                    SelectedCellNumber = Mathf.Clamp(SelectedCellNumber + (int)inputValue.x, 1, transform.childCount);
                    OnSelectedCellChanged(SelectedCell);
                }
            }
            else if (Mathf.Abs(inputValue.y) == 1)
            {
                SelectedCellNumber = Mathf.Clamp(SelectedCellNumber - CellsGridConstraintCount * (int)inputValue.y, 1, transform.childCount);
                OnSelectedCellChanged(SelectedCell);
            }
        }
    }
}