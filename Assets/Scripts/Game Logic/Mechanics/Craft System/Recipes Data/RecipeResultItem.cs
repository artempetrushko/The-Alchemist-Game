using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecipeResultItem : RecipeItem
{
    public ItemState GetResultItemState()
    {
        var resultItemState = item.GetItemState();
        if (resultItemState is StackableItemState)
        {
            (resultItemState as StackableItemState).ItemsCount = count;
        }
        return resultItemState;
    }
}
