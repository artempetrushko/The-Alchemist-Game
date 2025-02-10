using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCellsTemplateView : MonoBehaviour
{
    public ItemCellView[] IngredientCells => GetComponentsInChildren<ItemCellView>();
}
