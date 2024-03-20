using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesState : EquipmentState
{
    public int Defence { get; set; }

    public ClothesState(ClothesData clothes) : base(clothes)
    {
        Defence = clothes.BaseDefence;
    }

    public override object Clone() => new ClothesState(BaseParams as ClothesData)
    {
        //ItemData = ItemData,
        Description = Description,
        Aspects = Aspects,
        CastingDamage = CastingDamage,
        Effects = Effects,
        ComponentParts = ComponentParts,
        Endurance = Endurance,
        EnergyCapacity = EnergyCapacity,
        ImposedRune = ImposedRune,
        PoweredEnergyCount = PoweredEnergyCount,
        MaxRuneSize = MaxRuneSize,
        Defence = Defence,
    };

    public override Dictionary<string, string> GetItemParams()
    {
        return new();
    }
}
