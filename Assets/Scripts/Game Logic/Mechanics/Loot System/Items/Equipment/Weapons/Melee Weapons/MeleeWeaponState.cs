using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponState : WeaponState
{
    public float BlockingEfficiency { get; set; }

    public MeleeWeaponState(MeleeWeaponData meleeWeapon) : base(meleeWeapon)
    {
        BlockingEfficiency = meleeWeapon.BaseBlockingEfficiency;
    }

    public override object Clone() => new MeleeWeaponState(BaseParams as MeleeWeaponData)
    {
        //ItemData = ItemData,
        Description = Description,
        Aspects = Aspects,
        CastingDamage = CastingDamage,
        Effects = Effects,

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

        BlockingEfficiency = BlockingEfficiency,
    };
}
