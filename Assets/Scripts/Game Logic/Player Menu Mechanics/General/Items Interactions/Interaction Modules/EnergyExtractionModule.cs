using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyExtractionModule : ItemsInteractionModule, IInteractionExecutable, IInteractionCancelable
{
    [SerializeField]
    private PlayerMenuManager playerMenuManager;

    public override void StartInteraction(ItemSlot selectedItemSlot)
    {
        startItemSlot = selectedItemSlot;
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void CancelInteraction()
    {
        throw new System.NotImplementedException();
    }
}
