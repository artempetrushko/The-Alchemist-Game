using System;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsContainerMenuModel
    {
        public readonly ItemsContainer Container;
        public readonly (Item item, ContainedItemView itemView)[] ContainedItems;

        public event Action<int> SelectedItemNumberChanged;

        private int _selectedItemNumber;

        public int SelectedItemNumber
        {
            get => _selectedItemNumber;
            set
            {
                var clampedValue = Mathf.Clamp(value, 1, ContainedItems.Length);
                if (_selectedItemNumber != clampedValue)
                {
                    _selectedItemNumber = clampedValue;
                    SelectedItemNumberChanged?.Invoke(_selectedItemNumber);
                }
            }
        }

        public ItemsContainerMenuModel(ItemsContainer itemsContainer, (Item item, ContainedItemView itemView)[] containedItems)
        {
            Container = itemsContainer;
            ContainedItems = containedItems;
            SelectedItemNumber = 1;
        }
    }
}