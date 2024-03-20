using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableItemData : ItemData
{
    [Header("Параметры стакаемого предмета")]
    [SerializeField]
    protected int count = 1;
    [SerializeField]
    protected int stackMaxCount = 10;

    public int BaseCount => count;
    public int StackMaxCount => stackMaxCount;
}
