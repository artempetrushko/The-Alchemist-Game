using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryCategoryData
{
    [SerializeField]
    private Sprite categoryIcon;
    [SerializeField]
    private InventoryCategoryView categoryViewPrefab;

    public Sprite CategoryIcon => categoryIcon;
    public InventoryCategoryView CategoryViewPrefab => categoryViewPrefab;
}
