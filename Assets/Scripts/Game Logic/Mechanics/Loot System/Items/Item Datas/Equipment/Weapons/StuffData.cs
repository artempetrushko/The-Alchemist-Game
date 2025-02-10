using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StickAttackType
{
    Missile,
    Flow
}

[CreateAssetMenu(fileName = "New Stuff", menuName = "Game Entities/Items/Equipment/Weapon/Ranged Weapon/Stuff", order = 51)]
public class StuffData : RangedWeaponData
{
    [Header("Параметры для посоха")]
    [SerializeField]
    protected StickAttackType attackType;

    public StickAttackType BaseAttackType => attackType;

    public override ItemState GetItemState() => new StuffState(this);
}
