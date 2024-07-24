using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceState : StackableItemState
{
    protected ResourceState(ResourceData item, int itemsCount = 0) : base(item, itemsCount) { }
}
