using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipmentItemCellData
{
    [SerializeField]
    private WeaponItemCellData[] weaponItemCellDatas;
    [SerializeField]
    private ClothesItemCellData[] clothesItemCellDatas;

    public WeaponItemCellData[] WeaponItemCellDatas => weaponItemCellDatas;
    public ClothesItemCellData[] ClothesItemCellDatas => clothesItemCellDatas;
}
