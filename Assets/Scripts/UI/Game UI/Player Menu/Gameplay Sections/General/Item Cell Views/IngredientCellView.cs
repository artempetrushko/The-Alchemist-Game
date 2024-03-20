using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCellView : ItemCellView
{
    [Space, SerializeField]
    private CraftingItemsCounterModuleView craftingItemsCounterView;

    public void UpdateItemsCounter(int currentItemsCount, int? requiredItemsCount) => craftingItemsCounterView.UpdateItemsCounter(currentItemsCount, requiredItemsCount);
}
