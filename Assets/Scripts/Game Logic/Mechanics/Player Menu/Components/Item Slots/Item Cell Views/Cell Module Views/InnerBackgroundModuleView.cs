using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InnerBackgroundModuleView : ItemCellModuleView
{
    [SerializeField]
    protected Image backgroundIcon;

    public override void SetActive(bool isActive)
    {   
        backgroundIcon.gameObject.SetActive(isActive);
    }

    public override bool TryEnableWithNewItem(ItemState newItem)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateContent(ItemState attachedItem)
    {
        throw new System.NotImplementedException();
    }
}
