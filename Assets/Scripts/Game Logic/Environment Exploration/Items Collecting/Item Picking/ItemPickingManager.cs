using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickingManager : MonoBehaviour
{
    [SerializeField]
    private PlayerItemPicker playerItemPicker;
    [SerializeField]
    private ItemPickingMessagesManager itemPickingMessagesManager;
    [Space, SerializeField]
    private InventoryManager inventoryManager;

    public void PickItems(InputAction.CallbackContext context)
    {
        if (context.performed && playerItemPicker != null)
        {
            var pickableItems = playerItemPicker.PickableItems;
            if (pickableItems.Count > 0)
            {
                foreach (var item in pickableItems)
                {
                    if (inventoryManager.AddNewItemState(item.CurrentItemState))
                    {
                        itemPickingMessagesManager.ShowNewMessage(item.CurrentItemState);
                        Destroy(item.gameObject);
                    }
                }
                pickableItems.Clear();
            }
        }
    }
}
