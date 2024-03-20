using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class RecipeItem
{
    [SerializeField]
    protected ItemData item;
    [SerializeField]
    protected int count;
}
