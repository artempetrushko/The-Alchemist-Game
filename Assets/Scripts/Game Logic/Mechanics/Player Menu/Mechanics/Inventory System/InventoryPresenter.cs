using System;
using System.Linq;
using Controls;
using EventBus;
using GameLogic.LootSystem;
using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu.Inventory
{
    public class InventoryPresenter : IDisposable, IPlayerMenuSectionPresenter
    {
        private InventoryConfig _config;
        private InventoryModel _model;
        private InventoryView _mainInventoryView;
        private HUDEquipmentView _hudEquipmentView;
        private SignalBus _signalBus;

        public InventoryPresenter(InventoryView mainInventoryView, InventoryConfig config, HUDEquipmentView hudEquipmentView, SignalBus signalBus)
        {
            _config = config;
            _model = new InventoryModel()
            {
                MainInventorySlots = new MainInventorySlots(_config.InventorySlotsCount),
                QuickAccessSlots = new QuickAccessSlots(_config.QuickAccessSlotsCount, _signalBus),
                EquipmentSlots = new EquipmentSlots()
            };
            _mainInventoryView = mainInventoryView;
            _hudEquipmentView = hudEquipmentView;

            _signalBus = signalBus;
            _signalBus.Subscribe<ItemPickingRequestedSignal>(OnItemPickingRequested);
            _signalBus.Subscribe<PickableItemPickingRequestedSignal>(OnPickableItemPickingRequested);

            _signalBus.Subscribe<Player_SelectQuickAccessCellPerformedSignal>(SelectQuickAccessCell);
            _signalBus.Subscribe<Player_SelectNeighboringQuickAccessCellPerformedSignal>(SelectNeighboringQuickAccessCell);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ItemPickingRequestedSignal>(OnItemPickingRequested);
            _signalBus.Unsubscribe<PickableItemPickingRequestedSignal>(OnPickableItemPickingRequested);

            _signalBus.Unsubscribe<Player_SelectQuickAccessCellPerformedSignal>(SelectQuickAccessCell);
            _signalBus.Unsubscribe<Player_SelectNeighboringQuickAccessCellPerformedSignal>(SelectNeighboringQuickAccessCell);
        }

        public void Show()
        {
            _mainInventoryView.SetActive(true);
            BindItemSlotViews(_mainInventoryView);
        }

        public void Hide()
        {
            _mainInventoryView.SetActive(false);
        }

        private void BindItemSlotViews(InventoryView inventoryView)
        {
            var mainInventorySlotViews = inventoryView.MainInventoryCategoryView.CreateItemSlotViews(_model.MainInventorySlots.Slots.Length);
            for (var i = 0; i < _model.MainInventorySlots.Slots.Length; i++)
            {
                _model.MainInventorySlots.Slots[i].View = mainInventorySlotViews[i];
            }

            var quickAccessSlotViews = inventoryView.EquipmentCategoryView.QuickAccessSlotViews;
            for (var i = 0; i < _model.QuickAccessSlots.Slots.Length; i++)
            {
                _model.QuickAccessSlots.Slots[i].View = quickAccessSlotViews[i];
            }

            _model.EquipmentSlots.LeftWeaponSlot.View = inventoryView.EquipmentCategoryView.LeftWeaponSlotView;
            _model.EquipmentSlots.RightWeaponSlot.View = inventoryView.EquipmentCategoryView.RightWeaponSlotView;
            _model.EquipmentSlots.HatSlot.View = inventoryView.EquipmentCategoryView.HatSlotView;
            _model.EquipmentSlots.RaincoatSlot.View = inventoryView.EquipmentCategoryView.RaincoatSlotView;
            _model.EquipmentSlots.BootsSlot.View = inventoryView.EquipmentCategoryView.BootsSlotView;
            _model.EquipmentSlots.GlovesSlot.View = inventoryView.EquipmentCategoryView.GlovesSlotView;
            _model.EquipmentSlots.MedallionSlot.View = inventoryView.EquipmentCategoryView.MedallionSlotView;
        }

        public bool TryAddItem(Item item)
        {
            return item switch
            {
                StackableItem stackableItem => TryAddStackableItem(stackableItem),
                Equipment equipment => TryEquipOrAddItem(equipment),
                _ => TryPlaceItemToFreeSlot(item)
            };
        }

        public void DropItem(ItemSlot slot)
        {
            _signalBus.Fire(new ItemDroppedSignal(slot.ContainedItem));

            slot.Clear();
        }

        public void DropItem(ItemSlot slot, int itemsCount)
        {
            if (slot.ContainedItem is StackableItem stackableItem)
            {
                if (stackableItem.Count.Value == stackableItem.Count.MaxValue)
                {
                    DropItem(slot);
                    return;
                }

                var droppingItem = stackableItem.Clone();
                droppingItem.Count.Value = itemsCount;
                _signalBus.Fire(new ItemDroppedSignal(droppingItem));
            }
        }

        public void ChangeWeaponHandPosition(WeaponSlot currentWeaponSlot, WeaponHandPosition newWeaponPosition)
        {
            //TODO: переделать
            /*var newWeaponSlot = weaponSlots.FirstOrDefault(weaponSlot => weaponSlot.WeaponHandPosition == newWeaponPosition);
            newWeaponSlot?.TryPlaceOrSwapItem(currentWeaponSlot);*/
        }

        public void TakeEquipmentOff(Equipment equipment) => TryPlaceItemToFreeSlot(equipment);

        private bool TryAddStackableItem(StackableItem stackableItem)
        {
            var partialFreeSlots = _model.MainInventorySlots.Slots
                .Concat(_model.QuickAccessSlots.Slots)
                .Where(slot => slot.ContainedItem is StackableItem containedStackableItem
                        && stackableItem.Id == containedStackableItem.Id
                        && containedStackableItem.Count.Value > 0
                        && containedStackableItem.Count.Value < containedStackableItem.Count.MaxValue);
            if (partialFreeSlots.Count() > 0)
            {
                foreach (var containedStackableItem in partialFreeSlots.Select(slot => slot.ContainedItem as StackableItem).OrderByDescending(item => item.Count))
                {
                    var itemsCountToFillStack = containedStackableItem.Count.MaxValue - containedStackableItem.Count.Value;
                    if (stackableItem.Count.Value > itemsCountToFillStack)
                    {
                        containedStackableItem.Count.Value = containedStackableItem.Count.MaxValue;
                        stackableItem.Count.Value -= itemsCountToFillStack;
                    }
                    else
                    {
                        containedStackableItem.Count.Value += stackableItem.Count.Value;
                        return true;
                    }
                }
            }
            while (stackableItem.Count.Value > 0)
            {
                if (stackableItem.Count.Value <= stackableItem.Count.MaxValue)
                {
                    return TryPlaceItemToFreeSlot(stackableItem);
                }
                var newItemState = stackableItem.Clone() as StackableItem;
                newItemState.Count.Value = stackableItem.Count.MaxValue;
                if (TryPlaceItemToFreeSlot(newItemState))
                {
                    stackableItem.Count.Value -= stackableItem.Count.MaxValue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool TryEquipOrAddItem(Equipment equipment) => _model.EquipmentSlots.TryPlaceItem(equipment) || TryPlaceItemToFreeSlot(equipment);

        private bool TryPlaceItemToFreeSlot(Item item) => _model.MainInventorySlots.TryPlaceItem(item) || _model.QuickAccessSlots.TryPlaceItem(item);

        private bool TryPlaceItem(Item item, ItemSlot[] slotGroup) => slotGroup.Any(slot => slot.TryPlaceNewItem(item));

        private void OnPickableItemPickingRequested(PickableItemPickingRequestedSignal signal)
        {

        }

        private void OnItemPickingRequested(ItemPickingRequestedSignal signal)
        {

        }












        //private InventoryCategoryData[] categoryDatas;

        private PlayerMenuSectionSelectButton[] categoryButtons;
        private InventoryCategoryView currentInventoryCategoryView;
        private int currentItemCellsCategoryNumber;

        private InventoryCategoryView CurrentInventoryCategoryView
        {
            get => currentInventoryCategoryView;
            set
            {
                /*if (currentInventoryCategoryView != value)
                {
                    if (currentInventoryCategoryView != null)
                    {
                        craftInventorySubsectionView.ClearCategoryView();
                    }
                    currentInventoryCategoryView = craftInventorySubsectionView.CreateCategoryView(value);
                    inventoryManager.InitializeSelectedCategory(currentInventoryCategoryView);
                    currentInventoryCategoryView.SetParentSectionNavigation(categoriesParentSection);
                }*/
            }
        }
        private int CurrentItemCellsCategoryNumber
        {
            get => currentItemCellsCategoryNumber;
            set
            {
                /*if (value >= 1 && value <= categoryButtons.Length)
                {
                    if (currentItemCellsCategoryNumber != 0)
                    {
                        categoryButtons[currentItemCellsCategoryNumber - 1].SetInteractable(false);
                    }
                    currentItemCellsCategoryNumber = value;
                    if (currentItemCellsCategoryNumber != 0)
                    {
                        categoryButtons[currentItemCellsCategoryNumber - 1].SetInteractable(true);
                    }
                    CurrentInventoryCategoryView = categoryDatas[currentItemCellsCategoryNumber - 1].CategoryViewPrefab;
                }*/
            }
        }














        private int currentQuickAccessCellNumber;

        public int CurrentQuickAccessCellNumber
        {
            get => currentQuickAccessCellNumber;
            private set
            {
                /*if (value < 1)
                {
                    currentQuickAccessCellNumber = hudQuickAccessCellsContainer.transform.childCount;
                }
                else if (value > hudQuickAccessCellsContainer.transform.childCount)
                {
                    currentQuickAccessCellNumber = 1;
                }
                else
                {
                    currentQuickAccessCellNumber = value;
                }
                CurrentQuickAccessCell = hudQuickAccessCellsContainer.transform.GetChild(currentQuickAccessCellNumber - 1).GetComponent<ItemSlotView>();*/
            }
        }

        public ISelectableCollection DefaultSelectableCollection => throw new NotImplementedException();

        private void SelectQuickAccessCell(Player_SelectQuickAccessCellPerformedSignal signal)
        {
            if (int.TryParse(signal.Context.control.name, out int cellNumber))
            {
                CurrentQuickAccessCellNumber = cellNumber - 3;
            }
        }

        private void SelectNeighboringQuickAccessCell(Player_SelectNeighboringQuickAccessCellPerformedSignal signal)
        {
            var inputValue = signal.Context.ReadValue<Vector2>().x;
            if (Mathf.Abs(inputValue) == 1)
            {
                CurrentQuickAccessCellNumber += (int)inputValue;
            }
        }
    }
}