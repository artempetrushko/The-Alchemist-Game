using Controls;
using GameLogic.Inventory;
using System;
using UnityEngine.InputSystem;

public class ItemPickingManager : IDisposable
{
    private PlayerItemPicker _playerItemPicker;
    private ItemPickingMessagesManager _itemPickingMessagesPanelController;
    private InventoryManager _inventoryManager;
    private InputManager _inputManager;

    public ItemPickingManager(PlayerItemPicker playerItemPicker, ItemPickingMessagesManager itemPickingMessagesPanelController, 
        InventoryManager inventoryManager, InputManager inputManager)
    {
        _playerItemPicker = playerItemPicker;
        _itemPickingMessagesPanelController = itemPickingMessagesPanelController;
        _inventoryManager = inventoryManager;
        _inputManager = inputManager;

        _inputManager.PlayerActions.Player.PickItem.performed += PickItems;
    }

    public void Dispose()
    {
        _inputManager.PlayerActions.Player.PickItem.performed -= PickItems;
    }

    private void PickItems(InputAction.CallbackContext context)
    {
        if (_playerItemPicker != null)
        {
            var pickableItems = _playerItemPicker.PickableItems;
            if (pickableItems.Count > 0)
            {
                foreach (var item in pickableItems)
                {
                    if (_inventoryManager.AddNewItemState(item.CurrentItemState))
                    {
                        _itemPickingMessagesPanelController.ShowNewMessage(item.CurrentItemState);
                        //Destroy(item.gameObject);
                    }
                }
                pickableItems.Clear();
            }
        }
    }
}
