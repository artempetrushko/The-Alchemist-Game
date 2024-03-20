using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInventoryCategoryView : InventoryCategoryView
{
    public override void FillItemCellsContainer(int cellsCount)
    {
        base.FillItemCellsContainer(cellsCount);
        itemCellsContainer.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 1;
    }
}
