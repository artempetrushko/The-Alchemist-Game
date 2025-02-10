using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public abstract class CellsSubsectionNavigation : PlayerMenuSubsectionNavigation
    {
        public event Action<ItemCellView> SelectedCellChanged;

        protected ItemCellView currentSelectedCell;
        protected int currentSelectedCellNumber;

        public ItemCellView SelectedCell
        {
            get => currentSelectedCell;
            protected set
            {
                if (currentSelectedCell != value)
                {
                    if (currentSelectedCell != null)
                    {
                        currentSelectedCell.SetAppearance(false);
                        currentSelectedCell.OnCellDeselected();
                    }
                    currentSelectedCell = value;
                    if (currentSelectedCell != null)
                    {
                        currentSelectedCell.SetAppearance(true);
                        currentSelectedCellNumber = GetComponentsInChildren<ItemCellView>().ToList().IndexOf(currentSelectedCell) + 1;
                        currentSelectedCell.OnCellSelected();
                    }
                }
            }
        }
        public int SelectedCellNumber
        {
            get => currentSelectedCellNumber;
            protected set
            {
                currentSelectedCellNumber = value;
                SelectedCell = transform.GetChild(currentSelectedCellNumber - 1).GetComponent<ItemCellView>();
            }
        }

        public override void StartNavigation(SubsectionNavigationStartCondition condition = SubsectionNavigationStartCondition.Default)
        {
            SelectedCellNumber = 1;
        }

        public override void StopNavigation() => SelectedCell = null;

        public override void PressSelectedElement()
        {
            throw new NotImplementedException();
        }

        public void SelectItemCellByPointer(ItemCellView itemCell)
        {
            if (parentSection.CurrentSubsection != this)
            {
                parentSection.SetCurrentSubsection(this);
            }
            SelectedCell = itemCell;
        }

        public void OnSelectedCellChanged(ItemCellView itemCell)
        {
            SelectedCellChanged?.Invoke(itemCell);
        }
    }
}