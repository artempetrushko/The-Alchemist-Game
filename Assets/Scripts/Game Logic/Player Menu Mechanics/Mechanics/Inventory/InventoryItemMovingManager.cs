using Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic.Inventory
{
    public class InventoryItemMovingManager : IDisposable
    {
        private Transform movingItemContainer;
        private float movingItemCellScale = 1.15f;

        private InputManager _inputManager;

        private PlayerMenuManager playerMenuManager;

        private ItemSlot movingItemSlot;
        private InventoryNavigation inventorySubsection;
        private PlayerInputActionMap previousActionMap;

        public static bool IsMovingStarted { get; private set; }

        private ItemView MovingItemView => movingItemSlot.BaseItemState.ItemView;

        public InventoryItemMovingManager(InputManager inputManager)
        {
            _inputManager = inputManager;

            _inputManager.PlayerActions.PlayerMenuInventorySection.StartItemMoving.performed += StartItemMoving;
            _inputManager.PlayerActions.PlayerMenuInventoryItemMoving.PutItemDown.performed += FinishItemMoving;
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.PlayerMenuInventorySection.StartItemMoving.performed -= StartItemMoving;
            _inputManager.PlayerActions.PlayerMenuInventoryItemMoving.PutItemDown.performed -= FinishItemMoving;
        }

        private void StartItemMoving(InputAction.CallbackContext context)
        {
            var currentSubsection = playerMenuManager.CurrentSectionView.SectionNavigation.CurrentSubsection;
            if (currentSubsection is InventoryNavigation inventorySubsection)
            {
                var selectedItemSlot = inventorySubsection.SelectedCell.LinkedItemSlot;
                if (selectedItemSlot.BaseItemState != null)
                {
                    IsMovingStarted = true;
                    SetCursorActive(false);
                    movingItemSlot = selectedItemSlot;
                    MovingItemView.transform.localScale = new Vector3(movingItemCellScale, movingItemCellScale, 1);
                    MovingItemView.transform.SetParent(movingItemContainer);

                    this.inventorySubsection = inventorySubsection;
                    this.inventorySubsection.SelectedCell.DisableCellModules();
                    this.inventorySubsection.SelectedCellChanged += MoveItem;
                    previousActionMap = _inputManager.CurrentActionMap;
                    _inputManager.CurrentActionMap = PlayerInputActionMap.PlayerMenu_InventoryItemMoving;
                }
            }
        }

        private void MoveItem(ItemCellView destinationCell) => MovingItemView.transform.position = destinationCell.ItemViewContainer.transform.position;

        private void FinishItemMoving(InputAction.CallbackContext context)
        {
            IsMovingStarted = false;
            SetCursorActive(true);

            inventorySubsection.SelectedCell.LinkedItemSlot.TryPlaceOrSwapItem(movingItemSlot);
            movingItemSlot = null;
            inventorySubsection.SelectedCellChanged -= MoveItem;
            _inputManager.CurrentActionMap = previousActionMap;
        }

        private void SetCursorActive(bool isActive)
        {
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isActive;
        }
    }
}