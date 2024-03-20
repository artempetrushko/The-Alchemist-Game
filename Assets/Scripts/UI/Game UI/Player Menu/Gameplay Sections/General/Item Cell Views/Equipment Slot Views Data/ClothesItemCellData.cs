using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClothesItemCellData
{
    [SerializeField]
    private ClothesType clothesType;
    [SerializeField]
    private ItemCellView clothesCellView;

    public ClothesType ClothesType => clothesType;
    public ItemCellView ClothesCellView => clothesCellView;
}
