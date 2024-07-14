using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquippingModule : ItemsInteractionModule
{
    [SerializeField]
    private InventoryManager inventoryManager;

    public override void StartInteraction(ItemSlot selectedItemSlot)
    {
        inventoryManager.TryEquipInventoryItem(selectedItemSlot);
        OnInteractionExecuted();
    }
}
