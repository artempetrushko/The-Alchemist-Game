using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveItemButtonModuleView : ItemCellModuleView
{
    [SerializeField]
    private Button removeItemButton;

    public override void SetActive(bool isActive)
    {
        removeItemButton.gameObject.SetActive(isActive);
    }

    public override bool TryEnableWithNewItem(ItemState newItem)
    {
        return false;
    }

    public override void UpdateContent(ItemState attachedItem)
    {
        
    }
}
