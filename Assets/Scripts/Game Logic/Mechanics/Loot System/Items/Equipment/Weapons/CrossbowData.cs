using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crossbow", menuName = "Game Entities/Items/Equipment/Weapon/Ranged Weapon/Crossbow", order = 51)]
public class CrossbowData : RangedWeaponData
{
    public override ItemState GetItemState() => new CrossbowState(this);
}
