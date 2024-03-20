using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowState : RangedWeaponState
{
    public CrossbowState(CrossbowData crossbow) : base(crossbow) { }

    public override object Clone() => new CrossbowState(BaseParams as CrossbowData)
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

        Damage = Damage,
        Range = Range,
        Accuracy = Accuracy,
        AttackSpeed = AttackSpeed,
        CooldownTime = CooldownTime,
        PenetratingPower = PenetratingPower,

        MissileFlightSpeed = MissileFlightSpeed,
    };
}
