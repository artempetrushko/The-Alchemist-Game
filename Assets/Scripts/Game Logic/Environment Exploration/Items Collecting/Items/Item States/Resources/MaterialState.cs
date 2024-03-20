using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialState : ResourceState
{
    public MaterialState(MaterialData material, int itemsCount = 0) : base(material, itemsCount) { }

    public override object Clone() => new MaterialState(BaseParams as MaterialData, ItemsCount)
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
        return new();
    }
}
