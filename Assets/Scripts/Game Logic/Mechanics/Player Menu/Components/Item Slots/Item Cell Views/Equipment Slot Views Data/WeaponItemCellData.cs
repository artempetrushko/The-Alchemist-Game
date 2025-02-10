using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponItemCellData
{
    [SerializeField]
    private WeaponHandPosition weaponHandPosition;
    [SerializeField]
    private ItemCellView weaponCellView;

    public WeaponHandPosition WeaponHandPosition => weaponHandPosition;
    public ItemCellView WeaponCellView => weaponCellView;
}
