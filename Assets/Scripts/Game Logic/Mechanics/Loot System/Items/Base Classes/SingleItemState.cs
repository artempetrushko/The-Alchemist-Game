using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleItemState : ItemState
{
    protected SingleItemState(SingleItemData itemData) : base(itemData) { }
}
