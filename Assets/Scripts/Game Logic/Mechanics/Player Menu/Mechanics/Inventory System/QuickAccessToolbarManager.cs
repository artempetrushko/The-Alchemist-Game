using GameLogic.LootSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic.PlayerMenu
{
    public class QuickAccessToolbarManager : MonoBehaviour
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

        public void SelectQuickAccessCell(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (int.TryParse(context.control.name, out int cellNumber))
                {
                    CurrentQuickAccessCellNumber = cellNumber - 3;
                }
            }
        }

        public void SelectNeighboringQuickAccessCell(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var inputValue = context.ReadValue<Vector2>().x;
                if (inputValue == 1 || inputValue == -1)
                {
                    CurrentQuickAccessCellNumber += (int)inputValue;
                }
            }
        }

        public void CreateHUDItemCells(QuickAccessItemSlot[] quickAccessItemDatas, WeaponItemSlot[] weaponItemDatas)
        {
            foreach (var item in quickAccessItemDatas)
            {
                var hudItemCell = Instantiate(hudItemCellViewPrefab, hudQuickAccessCellsContainer.transform);
                item.LinkedHUDCellView = hudItemCell;
            }
            foreach (var item in weaponItemDatas)
            {
                var hudItemCell = Instantiate(hudItemCellViewPrefab, item.WeaponHandPosition switch
                {
                    WeaponHandPosition.Left => hudLeftWeaponItemCellsContainer.transform,
                    WeaponHandPosition.Right => hudRightWeaponItemCellsContainer.transform,
                });
                item.LinkedHUDCellView = hudItemCell;
            }
            CurrentQuickAccessCellNumber = 1;
        }
    }
}