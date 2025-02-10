using System.Collections;
using System.Collections.Generic;
using GameLogic.PlayerMenu;
using UnityEngine;

public class QuickAccessToolbarNavigation : CellsSubsectionNavigation
{
    public override void StartNavigation(SubsectionNavigationStartCondition condition = SubsectionNavigationStartCondition.Default)
    {
        SelectedCellNumber = condition switch
        {
            SubsectionNavigationStartCondition.TransitionFromTopSubsection => transform.childCount / 2,
            SubsectionNavigationStartCondition.TransitionFromRightSubsection => transform.childCount,
            SubsectionNavigationStartCondition.Default => 1
        };
    }

    public override void Navigate(Vector2 inputValue)
    {
        if (Mathf.Abs(inputValue.x) == 1)
        {
            if (SelectedCellNumber + inputValue.x > transform.childCount)
            {
                parentSection.SetCurrentSubsection(rightNeighboringSubsection, SubsectionNavigationStartCondition.TransitionFromLeftSubsection);             
            }
            else
            {
                SelectedCellNumber = Mathf.Clamp(SelectedCellNumber + (int)inputValue.x, 1, transform.childCount);
            }
        }
        else if (inputValue.y == 1)
        {
            parentSection.SetCurrentSubsection(topNeighboringSubsection, SubsectionNavigationStartCondition.TransitionFromBottomSubsection);
        }
    }
}
