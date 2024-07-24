using Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuickAccessToolbarManager : IDisposable
{
    [SerializeField]
    private ItemCellView hudItemCellViewPrefab;
    [SerializeField]
    private GameObject hudQuickAccessCellsContainer;
    [SerializeField]
    private GameObject hudLeftWeaponItemCellsContainer;
    [SerializeField]
    private GameObject hudRightWeaponItemCellsContainer;

    private ItemCellView currentQuickAccessCell;
    private int currentQuickAccessCellNumber;

    private InputManager _inputManager;

    public ItemCellView CurrentQuickAccessCell
    {
        get => currentQuickAccessCell;
        private set
        {
            if (currentQuickAccessCell != null)
            {
                currentQuickAccessCell.SetAppearance(false);
            }
            currentQuickAccessCell = value;
            currentQuickAccessCell.SetAppearance(true);
        }
    }

    public int CurrentQuickAccessCellNumber
    {
        get => currentQuickAccessCellNumber;
        private set
        {
            if (value < 1)
            {
                currentQuickAccessCellNumber = transform.childCount;
            }
            else if (value > transform.childCount)
            {
                currentQuickAccessCellNumber = 1;
            }
            else
            {
                currentQuickAccessCellNumber = value;
            }
            CurrentQuickAccessCell = hudQuickAccessCellsContainer.transform.GetChild(currentQuickAccessCellNumber - 1).GetComponent<ItemCellView>();
        }
    }

    public QuickAccessToolbarManager()
    {
        _inputManager.PlayerActions.Player.SelectQuickAccessCell.performed += SelectQuickAccessCell;
        _inputManager.PlayerActions.Player.SelectNeighboringQuickAccessCell.performed += SelectNeighboringQuickAccessCell;
    }

    public void Dispose()
    {
        _inputManager.PlayerActions.Player.SelectQuickAccessCell.performed -= SelectQuickAccessCell;
        _inputManager.PlayerActions.Player.SelectNeighboringQuickAccessCell.performed -= SelectNeighboringQuickAccessCell;
    }

    private void SelectQuickAccessCell(InputAction.CallbackContext context)
    {
        if (int.TryParse(context.control.name, out int cellNumber))
        {
            CurrentQuickAccessCellNumber = cellNumber - 3;
        }
    }

    private void SelectNeighboringQuickAccessCell(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>().x;
        if (Mathf.Abs(inputValue) == 1)
        {
            CurrentQuickAccessCellNumber += (int)inputValue;
        }
    }

    public void CreateHUDItemCells(QuickAccessItemSlot[] quickAccessItemDatas, WeaponItemSlot[] weaponItemDatas)
    {
        foreach (var item in quickAccessItemDatas)
        {
            var hudItemSlot = Instantiate(hudItemCellViewPrefab, hudQuickAccessCellsContainer.transform);
            item.LinkedHUDCellView = hudItemSlot;
        }
        foreach (var item in weaponItemDatas)
        {
            var hudItemSlot = Instantiate(hudItemCellViewPrefab, item.WeaponHandPosition switch
            {
                WeaponHandPosition.Left => hudLeftWeaponItemCellsContainer.transform,
                WeaponHandPosition.Right => hudRightWeaponItemCellsContainer.transform,
            });
            item.LinkedHUDCellView = hudItemSlot;
        }
        CurrentQuickAccessCellNumber = 1;
    }
}
