using Controls;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemsContainersManager : MonoBehaviour
{
    [SerializeField]
    private ItemsContainerContentView itemsContainerContentViewPrefab;
    [SerializeField]
    private GameObject itemsContainerContentViewContainer;
    [Space, SerializeField]
    private InventoryManager inventoryManager;

    private InputManager _inputManager;

    [SerializeField]
    private ItemPickingMessagesManager itemPickingMessagesManager; 

    private ItemsContainerContentView itemsContainerContentView;
    private ItemsContainer currentItemsContainer;
    private List<(ItemState item, ContainedItemView itemView)> currentContainingItems;
    private (ItemState item, ContainedItemView itemView) currentSelectedItem;
    private int currentSelectedItemNumber;

    private (ItemState item, ContainedItemView itemView) CurrentSelectedItem
    {
        get => currentSelectedItem;
        set
        {
            if (currentSelectedItem != value)
            {
                currentSelectedItem = value;
                var selectedItemNumber = currentContainingItems.IndexOf(currentSelectedItem) + 1;
                if (selectedItemNumber != CurrentSelectedItemNumber)
                {
                    currentSelectedItemNumber = selectedItemNumber;
                }
                itemsContainerContentView.SelectContainedItemView(currentSelectedItem.itemView, 1 - (float)CurrentSelectedItemNumber / currentContainingItems.Count);          
            }
        }
    }
    private int CurrentSelectedItemNumber
    {
        get => currentSelectedItemNumber;
        set
        {
            if (currentSelectedItemNumber != value)
            {
                currentSelectedItemNumber = Mathf.Clamp(value, 1, currentContainingItems.Count);
                CurrentSelectedItem = currentContainingItems[currentSelectedItemNumber - 1];
            }
        }
    }

    public ItemsContainersManager(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    public void OpenContainer(ItemsContainer itemsContainer)
    {
        currentItemsContainer = itemsContainer;
        itemsContainerContentView = Instantiate(itemsContainerContentViewPrefab, itemsContainerContentViewContainer.transform);
        itemsContainerContentView.SetInfo(currentItemsContainer.Title);
        currentContainingItems = itemsContainerContentView.CreateContainedItemViews(currentItemsContainer.GetContainingItems(), PickItem, (containingItem) => { CurrentSelectedItem = containingItem; });
        CurrentSelectedItemNumber = 1;

        _inputManager.CurrentActionMap = PlayerInputActionMap.HUD_ItemsContainer;
        ShowControlsTips(itemsContainerContentView.ControlsTipsSectionView);
        _inputManager.SubscribeControlsChangedEvent(() => ShowControlsTips(itemsContainerContentView.ControlsTipsSectionView));
    }

    public void CloseContainer(InputAction.CallbackContext context)
    {      
        if (context.performed)
        {
            Destroy(itemsContainerContentView.gameObject);
            if (currentContainingItems.Count == 0)
            {
                currentItemsContainer.SetFilledContainerEffectActive(false);
                Destroy(currentItemsContainer);
            }

            _inputManager.UnsubscribeControlsChangedEvent(() => ShowControlsTips(itemsContainerContentView.ControlsTipsSectionView));
            _inputManager.CurrentActionMap = PlayerInputActionMap.Player;
        }      
    }

    public void ChangeSelectedContainedItem(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>().y;
        CurrentSelectedItemNumber = Mathf.Clamp(CurrentSelectedItemNumber - (int)inputValue, 1, currentContainingItems.Count);
    } 

    public void PickItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PickItem(CurrentSelectedItem);
        }
    }

    public void PickAllItems(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PickAllItems();
        }
    }

    private void PickItem((ItemState item, ContainedItemView itemView) containingItem)
    {
        if (inventoryManager.AddNewItemState(containingItem.item))
        {
            itemPickingMessagesManager.ShowNewMessage(containingItem.item);
            Destroy(containingItem.itemView.gameObject);
            currentContainingItems.Remove(containingItem);
            if (currentContainingItems.Count > 0)
            {
                CurrentSelectedItemNumber--;
            }         
        }      
    }

    private void PickAllItems()
    {
        for (var i = currentContainingItems.Count - 1; i >= 0; i--)
        {
            PickItem(currentContainingItems[i]);
        }
    }

    private void ShowControlsTips(ControlsTipsSectionView controlsTipsSectionView)
    {
        _inputManager.ShowCurrentControlsTips(controlsTipsSectionView, new[]
        {
            ("Взять", _inputManager.PlayerActions.HUDItemsContainer.Take),
            ("Взять всё", _inputManager.PlayerActions.HUDItemsContainer.TakeAll),
            ("Закрыть", _inputManager.PlayerActions.HUDItemsContainer.CloseContainer)
        });
    }
}