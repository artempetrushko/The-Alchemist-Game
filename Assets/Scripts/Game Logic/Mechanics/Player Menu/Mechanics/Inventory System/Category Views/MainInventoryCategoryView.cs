using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class MainInventoryCategoryView : InventoryCategoryView
    {
        public override void FillItemCellsContainer(int cellsCount)
        {
            base.FillItemCellsContainer(cellsCount);
            itemCellsContainer.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 1;
        }
    }
}