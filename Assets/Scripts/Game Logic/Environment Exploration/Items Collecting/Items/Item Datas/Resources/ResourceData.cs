using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RareClass
{
    Junk,
    Ordinary,
    Rare,
    Precious
}

public abstract class ResourceData : StackableItemData
{
    [Header("Параметры ресурса")]
    [SerializeField]
    protected RareClass rare;
    [SerializeField]
    protected bool isShredded;

    public RareClass Rare => rare;
    public bool IsShredded => isShredded;
}
