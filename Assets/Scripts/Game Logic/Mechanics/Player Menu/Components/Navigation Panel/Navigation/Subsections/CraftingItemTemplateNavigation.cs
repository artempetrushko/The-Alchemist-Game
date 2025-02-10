using System.Collections;
using System.Collections.Generic;
using GameLogic.PlayerMenu;
using UnityEngine;

public class CraftingItemTemplateNavigation : CellsSubsectionNavigation
{
    public override void Navigate(Vector2 inputValue)
    {
        if (Mathf.Abs(inputValue.x) == 1)
        {
           SelectedCellNumber += (int)inputValue.x;
        }
        else if (Mathf.Abs(inputValue.y) == 1)
        {
            SelectedCellNumber -= (int)inputValue.y;
        }
    }
}
