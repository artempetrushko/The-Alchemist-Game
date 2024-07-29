using Controls;
using GameLogic.Inventory;
using System;
using System.Collections.Generic;
using UI.Environment;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsContainersManager : IDisposable
    {
        private ItemsContainerContentPanelView itemsContainerContentView;
        private GameObject itemsContainerContentViewContainer;
        private InventoryManager inventoryManager;

        private InputManager _inputManager;

        private ItemPickingMessagesManager itemPickingMessagesManager;

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

            _inputManager.PlayerActions.HUDItemsContainer.CloseContainer.performed += CloseContainer;
            _inputManager.PlayerActions.HUDItemsContainer.Navigate.performed += ChangeSelectedContainedItem;
            _inputManager.PlayerActions.HUDItemsContainer.Take.performed += PickItem;
            _inputManager.PlayerActions.HUDItemsContainer.TakeAll.performed += PickAllItems;
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.HUDItemsContainer.CloseContainer.performed -= CloseContainer;
            _inputManager.PlayerActions.HUDItemsContainer.Navigate.performed -= ChangeSelectedContainedItem;
            _inputManager.PlayerActions.HUDItemsContainer.Take.performed -= PickItem;
            _inputManager.PlayerActions.HUDItemsContainer.TakeAll.performed -= PickAllItems;
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

        private void CloseContainer(InputAction.CallbackContext context)
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

        private void ChangeSelectedContainedItem(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>().y;
            CurrentSelectedItemNumber = Mathf.Clamp(CurrentSelectedItemNumber - (int)inputValue, 1, currentContainingItems.Count);
        }

        private void PickItem(InputAction.CallbackContext context) => PickItem(CurrentSelectedItem);

        private void PickAllItems(InputAction.CallbackContext context)
        {
            for (var i = currentContainingItems.Count - 1; i >= 0; i--)
            {
                PickItem(currentContainingItems[i]);
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
}