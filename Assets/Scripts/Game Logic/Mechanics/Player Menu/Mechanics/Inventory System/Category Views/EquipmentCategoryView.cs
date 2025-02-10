using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentCategoryView : InventoryCategoryView
{
    [SerializeField]
    private EquipmentItemCellData equipmentItemCellData;

    public override ItemCellView[][] AllItemCells => base.AllItemCells.Concat(new ItemCellView[][] { WeaponCells, ClothesCells }).ToArray();
    public ItemCellView[] WeaponCells => equipmentItemCellData.WeaponItemCellDatas.Select(cellData => cellData.WeaponCellView).ToArray();
    public ItemCellView[] ClothesCells => equipmentItemCellData.ClothesItemCellDatas.Select(cellData => cellData.ClothesCellView).ToArray();
}
