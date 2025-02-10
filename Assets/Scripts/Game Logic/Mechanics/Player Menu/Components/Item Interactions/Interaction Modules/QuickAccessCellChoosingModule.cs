using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAccessCellChoosingModule : ItemsInteractionModule, IInteractionExecutable
{
    public override void StartInteraction(ItemSlot selectedItemSlot)
    {
        //playerMenuSubsection.transform.SetAsLastSibling();
        //playerMenuSubsection.StartNavigation_WeaponEquipping();
        //weaponCellContainers[0].transform.SetAsLastSibling();
        //weaponCellContainers[1].transform.SetAsLastSibling();
    }

    public override void CancelInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        //playerSetSubsection.SelectedCell.GetComponent<ItemCellContainer>().SwapAndPlaceItem(currentItemCell);
    }
}
