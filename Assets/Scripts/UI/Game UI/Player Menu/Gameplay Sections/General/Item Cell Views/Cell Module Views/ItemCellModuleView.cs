using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCellModuleView : MonoBehaviour
{
    public abstract void SetActive(bool isActive);

    public abstract bool TryEnableWithNewItem(ItemState newItem);

    public abstract void UpdateContent(ItemState attachedItem);
}
