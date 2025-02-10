using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyCounterModuleView : ItemCellModuleView
{
    [SerializeField]
    private TMP_Text energyCounterText;
    [SerializeField]
    private GameObject energyCounterContainer;

    public override void SetActive(bool isActive)
    {
        energyCounterContainer.SetActive(isActive);
    }

    public override bool TryEnableWithNewItem(ItemState newItem)
    {
        return false;
    }

    public override void UpdateContent(ItemState attachedItem)
    {
        energyCounterText.text = attachedItem switch
        {
            StackableItemState stackableItem => stackableItem.TotalContainedEnergyCount.ToString(),
            _ => attachedItem.ContainedEnergyCount.ToString()
        };
    }
}
