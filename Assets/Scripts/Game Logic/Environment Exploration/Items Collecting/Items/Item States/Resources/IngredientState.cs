using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientState : ResourceState
{
    public IngredientState(IngredientData ingredient, int itemsCount = 0) : base(ingredient, itemsCount) { }

    public override object Clone() => new IngredientState(BaseParams as IngredientData, ItemsCount)
    {
        //ItemData = ItemData,
        ItemsCount = ItemsCount,
        MaxStackItemsCount = MaxStackItemsCount,
        Description = Description,
        Aspects = Aspects,
        CastingDamage = CastingDamage,
        Effects = Effects,
    };

    public override Dictionary<string, string> GetItemParams()
    {
        return new Dictionary<string, string>();
    }
}
