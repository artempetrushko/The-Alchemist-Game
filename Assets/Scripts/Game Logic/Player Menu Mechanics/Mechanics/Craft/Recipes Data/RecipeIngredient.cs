using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecipeIngredient : RecipeItem
{
    public int Count => count;

    public bool CheckSelectedItemMatching(ItemState selectedItem, bool isCountMatchingRequired = false)
    {
        if (isCountMatchingRequired)
        {
            return selectedItem switch
            {
                SingleItemState => selectedItem.BaseParams.Equals(item),
                StackableItemState => selectedItem.BaseParams.Equals(item) && (selectedItem as StackableItemState).ItemsCount >= count
            };
        }
        return selectedItem.BaseParams.Equals(item);
    }
}
