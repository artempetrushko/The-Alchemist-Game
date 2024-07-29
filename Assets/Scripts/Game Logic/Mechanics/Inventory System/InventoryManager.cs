using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UI.PlayerMenu;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace GameLogic.Inventory
{
    public class InventoryManager
    {
        private WeaponItemSlot[] weaponSlots;
        private ClothesItemSlot[] clothesSlots;
        
        private Transform droppedItemsSpawnPoint;
        
        private EquipmentManager equipmentManager;
        private QuickAccessToolbarManager quickAccessToolbarManager;
        private ItemDescriptionPanelController itemDescriptionsManager;
        private InventoryItemsInteractionsManager inventoryItemsInteractionsManager;

        private SimpleItemSlot[] mainInventorySlots;
        private QuickAccessItemSlot[] quickAccessSlots;
        private InventorySectionView currentInventoryView;

        public InventoryManager()
        {

        }

        public void Initialize()
        {
            //mainInventorySlots = new SimpleItemSlot[inventorySlotsCount].Select(x => new SimpleItemSlot()).ToArray();
            //quickAccessSlots = new QuickAccessItemSlot[quickAccessSlotsCount].Select(x => new QuickAccessItemSlot()).ToArray();
            quickAccessToolbarManager.CreateHUDItemCells(quickAccessSlots, weaponSlots);
            equipmentManager.SubscribeWeaponItemDatas(weaponSlots);
        }

        public void InitializeLinkedView(PlayerMenuSectionView mechanicsLinkedView)
        {
            if (mechanicsLinkedView is InventorySectionView inventoryView)
            {
                currentInventoryView = inventoryView;
                //currentInventoryView.InventorySubsectionView.GetCategoryView<MainInventoryCategoryView>()?.FillItemCellsContainer(inventorySlotsCount);
                //currentInventoryView.InventorySubsectionView.GetCategoryView<EquipmentCategoryView>()?.FillItemCellsContainer(quickAccessSlotsCount);
                AttachInventoryItemCellsToItems(currentInventoryView);

                foreach (var items in currentInventoryView.InventorySubsectionView.AllInventoryCells)
                {
                    SubscribeOnInventoryItemCellsEvents(items);
                }
            }
        }

        public void InitializeSelectedCategory(InventoryCategoryView categoryView)
        {
            switch (categoryView)
            {
                case MainInventoryCategoryView mainInventoryCategoryView:
                    // mainInventoryCategoryView.FillItemCellsContainer(inventorySlotsCount);
                    AttachItemCellsToItems(mainInventoryCategoryView.MainItemCells, mainInventorySlots);
                    break;

                case EquipmentCategoryView equipmentCategoryView:
                    //equipmentCategoryView.FillItemCellsContainer(quickAccessSlotsCount);
                    AttachItemCellsToItems(equipmentCategoryView.MainItemCells, quickAccessSlots);
                    AttachItemCellsToItems(equipmentCategoryView.WeaponCells, weaponSlots);
                    AttachItemCellsToItems(equipmentCategoryView.ClothesCells, clothesSlots);
                    break;
            }
        }

        public bool AddNewItemState<T>(T itemState) where T : ItemState
        {
            return itemState switch
            {
                SingleItemState singleItem => singleItem switch
                {
                    EquipmentState equipment => TryEquipOrAddItem(equipment),
                    _ => TryAddItem(singleItem),
                },
                StackableItemState stackableItem => TryAddStackableItem(stackableItem),
                _ => false,
            };
        }

        public bool TryAddStackableItemCopy<T>(T baseItemState, int newItemsCount) where T : StackableItemState
        {
            var newItemState = baseItemState.Clone() as T;
            newItemState.ItemsCount = newItemsCount;
            return TryAddItem(newItemState);
        }

        public void DropItem(ItemState item)
        {
            CreateDroppedItem(item);
            item.LinkedItemSlot.ClearItemState();
        }

        public void DropItem(StackableItemState stackableItem, int itemsCount)
        {
            stackableItem.ItemsCount -= itemsCount;

            var droppedItemState = stackableItem.Clone() as StackableItemState;
            droppedItemState.ItemsCount = itemsCount;
            CreateDroppedItem(droppedItemState);
        }

        public void ChangeWeaponHandPosition(WeaponItemSlot currentWeaponSlot, WeaponHandPosition newWeaponPosition)
        {
            var newWeaponSlot = weaponSlots.FirstOrDefault(weaponSlot => weaponSlot.WeaponHandPosition == newWeaponPosition);
            newWeaponSlot?.TryPlaceOrSwapItem(currentWeaponSlot);
        }

        public void TakeEquipmentOff(EquipmentState equipment) => TryAddItem(equipment);

        public bool TryEquipInventoryItem(ItemSlot previousItemSlot)
        {
            if (previousItemSlot.BaseItemState is EquipmentState equipment)
            {
                return TryEquipItem(equipment);
            }
            return false;
        }

        private bool TryAddStackableItem(StackableItemState stackableItem)
        {
            var inventoryItemDatas = mainInventorySlots.Select(item => item as ItemSlot).Concat(quickAccessSlots.Select(item => item as ItemSlot));
            var partialFilledStacks = inventoryItemDatas
                .Where(itemData => itemData.BaseItemState is StackableItemState stackableItemData
                        && stackableItemData.BaseParams.ID == stackableItem.BaseParams.ID
                        && stackableItemData.ItemsCount < stackableItemData.MaxStackItemsCount);
            if (partialFilledStacks.Count() > 0)
            {
                foreach (var containedStackableItem in partialFilledStacks.Select(stack => stack.BaseItemState as StackableItemState).OrderByDescending(item => item.ItemsCount))
                {
                    var itemsCountToFillStack = containedStackableItem.MaxStackItemsCount - containedStackableItem.ItemsCount;
                    if (stackableItem.ItemsCount > itemsCountToFillStack)
                    {
                        containedStackableItem.ItemsCount = containedStackableItem.MaxStackItemsCount;
                        stackableItem.ItemsCount -= itemsCountToFillStack;
                    }
                    else
                    {
                        containedStackableItem.ItemsCount += stackableItem.ItemsCount;
                        return true;
                    }
                }
            }
            while (stackableItem.ItemsCount > 0)
            {
                if (stackableItem.ItemsCount <= stackableItem.MaxStackItemsCount)
                {
                    return TryAddItem(stackableItem);
                }
                var newItemState = stackableItem.Clone() as StackableItemState;
                newItemState.ItemsCount = stackableItem.MaxStackItemsCount;
                if (TryAddItem(newItemState))
                {
                    stackableItem.ItemsCount -= stackableItem.MaxStackItemsCount;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool TryEquipOrAddItem(EquipmentState equipment) => TryEquipItem(equipment) || TryAddItem(equipment);

        private bool TryEquipItem(EquipmentState equipment)
        {
            return equipment switch
            {
                WeaponState weapon => TryAddItemToCollection(weaponSlots, weapon, (weaponSlot) => weaponSlot.ItemState == null),
                ClothesState clothes => TryAddItemToCollection(clothesSlots, clothes, (clothesSlot) => clothesSlot.ItemState == null && clothesSlot.ClothesType == (clothes.BaseParams as ClothesData).ClothesType)
            };
        }

        private bool TryAddItem(ItemState item) =>
            TryAddItemToCollection(quickAccessSlots, item, (slot) => slot.ItemState == null)
            || TryAddItemToCollection(mainInventorySlots, item, (slot) => slot.ItemState == null);

        private bool TryAddItemToCollection<T, P>(T[] itemSlots, P item, Func<T, bool> itemAddingCondition)
            where T : ItemSlot<P>
            where P : ItemState
        {
            var freeItemSlot = itemSlots.FirstOrDefault(itemSlot => itemAddingCondition(itemSlot));
            if (freeItemSlot != null)
            {
                return freeItemSlot.TryPlaceItem(item);
            }
            return false;
        }

        private void AttachInventoryItemCellsToItems(InventorySectionView inventoryView)
        {
            AttachItemCellsToItems(inventoryView.InventorySubsectionView.GetCategoryView<MainInventoryCategoryView>().MainItemCells, mainInventorySlots);
            AttachItemCellsToItems(inventoryView.InventorySubsectionView.GetCategoryView<EquipmentCategoryView>().MainItemCells, quickAccessSlots);
            AttachItemCellsToItems(inventoryView.InventorySubsectionView.GetCategoryView<EquipmentCategoryView>().WeaponCells, weaponSlots);
            AttachItemCellsToItems(inventoryView.InventorySubsectionView.GetCategoryView<EquipmentCategoryView>().ClothesCells, clothesSlots);
        }

        private void AttachItemCellsToItems(ItemCellView[] itemCells, ItemSlot[] inventoryItems)
        {
            for (var i = 0; i < inventoryItems.Length; i++)
            {
                inventoryItems[i].CellView = itemCells[i];
            }
        }

        private void SubscribeOnInventoryItemCellsEvents(ItemCellView[] itemCells)
        {
            foreach (var itemCell in itemCells)
            {
                itemCell.CellSelected += () =>
                {
                    var inventoryItemData = itemCell.LinkedItemSlot;
                    if (inventoryItemData.BaseItemState != null)
                    {
                        itemDescriptionsManager.ShowPanel(inventoryItemData.BaseItemState, itemCell).Forget();
                    }
                };
                itemCell.CellDeselected += () => itemDescriptionsManager.HidePanel();

                itemCell.AddEventTriggerListener(EventTriggerType.PointerDown, (eventData) =>
                {
                    if ((eventData as PointerEventData).button == PointerEventData.InputButton.Right)
                    {
                        var selectedItemSlot = itemCell.LinkedItemSlot;
                        if (selectedItemSlot.BaseItemState != null)
                        {
                            inventoryItemsInteractionsManager.CreateItemSlotActionsMenu(selectedItemSlot);
                        }
                    }
                });
                itemCell.AddEventTriggerListener(EventTriggerType.Drop, (eventData) => itemCell.LinkedItemSlot.TryPlaceOrSwapItem(ItemViewDraggingModule.DraggingItem.LinkedItem.LinkedItemSlot));
            }
        }

        private void CreateDroppedItem(ItemState itemState)
        {
            var physicalItem = Object.Instantiate(itemState.BaseParams.PhysicalRepresentation);
            physicalItem.transform.position = droppedItemsSpawnPoint.position;
            physicalItem.CurrentItemState = itemState;
        }
    }
}