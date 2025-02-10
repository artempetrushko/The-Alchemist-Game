using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class IngredientCellsTemplateView : MonoBehaviour
    {
        public ItemCellView[] IngredientCells => GetComponentsInChildren<ItemCellView>();
    }
}