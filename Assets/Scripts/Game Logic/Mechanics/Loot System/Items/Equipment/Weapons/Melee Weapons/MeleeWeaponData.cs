using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Game Entities/Items/Equipment/Weapon/Melee Weapon", order = 51)]
public class MeleeWeaponData : WeaponData
{
    [Header("��������� ������ �������� ���")]
    [SerializeField]
    protected float blockingEfficiency;

    public float BaseBlockingEfficiency => blockingEfficiency;

    public override ItemState GetItemState() => new MeleeWeaponState(this);
}
