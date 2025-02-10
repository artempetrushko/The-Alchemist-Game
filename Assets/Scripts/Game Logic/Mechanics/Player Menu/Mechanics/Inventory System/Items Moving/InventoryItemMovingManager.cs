using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryItemMovingManager : MonoBehaviour
{
    [SerializeField]
    private Transform movingItemContainer;
    [SerializeField]
    private float movingItemCellScale = 1.15f;
    [Space, SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private PlayerMenuManager playerMenuManager;

    private ItemSlot movingItemSlot;
    private InventoryNavigation inventorySubsection;
    private PlayerInputActionMap previousActionMap;
    
    public static bool IsMovingStarted { get; private set; }

    private ItemView MovingItemView => movingItemSlot.BaseItemState.ItemView;

    public void StartItemMoving(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartItemMoving();
        }
    }

    public void FinishItemMoving(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FinishItemMoving();
        }
    }

    private void StartItemMoving()
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
                previousActionMap = inputManager.CurrentActionMap;
                inputManager.CurrentActionMap = PlayerInputActionMap.PlayerMenu_InventoryItemMoving;
            }
        }
    }

    private void MoveItem(ItemCellView destinationCell) => MovingItemView.transform.position = destinationCell.ItemViewContainer.transform.position;

    private void FinishItemMoving()
    {
        IsMovingStarted = false;
        SetCursorActive(true);

        inventorySubsection.SelectedCell.LinkedItemSlot.TryPlaceOrSwapItem(movingItemSlot);
        movingItemSlot = null;
        inventorySubsection.SelectedCellChanged -= MoveItem;
        inputManager.CurrentActionMap = previousActionMap;
    }

    private void SetCursorActive(bool isActive)
    {
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isActive;
    }
}
