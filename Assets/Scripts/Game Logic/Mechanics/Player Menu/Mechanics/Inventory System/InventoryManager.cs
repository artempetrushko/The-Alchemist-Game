using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : PlayerMenuMechanicsManager
{
    [SerializeField]
    private int inventoryCellsCount;
    [SerializeField]
    private int quickAccessCellsCount;
    [SerializeField]
    private WeaponItemSlot[] weaponItems;
    [SerializeField]
    private ClothesItemSlot[] clothesItems;
    [Space, SerializeField]
    private Transform droppedItemsSpawnPoint;
    [Space, SerializeField]
    private EquipmentManager equipmentManager;
    [SerializeField]
    private QuickAccessToolbarManager quickAccessToolbarManager;
    [SerializeField]
    private ItemDescriptionsManager itemDescriptionsManager;
    [SerializeField]
    private InventoryItemsInteractionsManager inventoryItemsInteractionsManager;

    private SimpleItemSlot[] inventoryItems;
    private QuickAccessItemSlot[] quickAccessItems;
    private InventorySectionView currentInventoryView;

    public override void InitializeLinkedView(PlayerMenuSectionView mechanicsLinkedView)
    {
        if (mechanicsLinkedView is InventorySectionView inventoryView)
        {
            currentInventoryView = inventoryView;
            currentInventoryView.InventorySubsectionView.GetCategoryView<MainInventoryCategoryView>()?.FillItemCellsContainer(inventoryCellsCount);
            currentInventoryView.InventorySubsectionView.GetCategoryView<EquipmentCategoryView>()?.FillItemCellsContainer(quickAccessCellsCount);
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
                mainInventoryCategoryView.FillItemCellsContainer(inventoryCellsCount);
                AttachItemCellsToItems(mainInventoryCategoryView.MainItemCells, inventoryItems);
                break;

            case EquipmentCategoryView equipmentCategoryView:
                equipmentCategoryView.FillItemCellsContainer(quickAccessCellsCount);
                AttachItemCellsToItems(equipmentCategoryView.MainItemCells, quickAccessItems);
                AttachItemCellsToItems(equipmentCategoryView.WeaponCells, weaponItems);
                AttachItemCellsToItems(equipmentCategoryView.ClothesCells, clothesItems);
                break;
        }
    }

    public void Initialize()
    {
        inventoryItems = new SimpleItemSlot[inventoryCellsCount].Select(x => new SimpleItemSlot()).ToArray();
        quickAccessItems = new QuickAccessItemSlot[quickAccessCellsCount].Select(x => new QuickAccessItemSlot()).ToArray();
        quickAccessToolbarManager.CreateHUDItemCells(quickAccessItems, weaponItems);
        equipmentManager.SubscribeWeaponItemDatas(weaponItems);
    }

    public void AddSomeItems(List<ItemState> items)
    {
        foreach (var item in items)
        {
            AddNewItemState(item);
        }
    }

    public bool AddNewItemState<T>(T itemState) where T : ItemState
    {
        return itemState switch
        {
            StackableItemState stackableItem => TryAddStackableItem(stackableItem),
            EquipmentState equipment => TryEquipOrAddItem(equipment),
            _ => TryAddItem(itemState)
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

    public void CreateDroppedItem(ItemState itemState)
    {
        var physicalItem = Instantiate(itemState.BaseParams.PhysicalRepresentation);
        physicalItem.transform.position = droppedItemsSpawnPoint.position;
        physicalItem.CurrentItemState = itemState;
    }

    /*public void RemoveAttachedItem()
    {
        var freeInventoryCells = inventorySection.GetComponentsInChildren<SimpleItemCellContainer>().Where(container => container.IsItemPlaceEmpty).ToList();
        if (freeInventoryCells.Count > 0)
        {
            freeInventoryCells[0].PlaceItem(ContainedItem);
        }
    }*/

    public void RemoveItem(ItemState itemState) { }

    public void RemoveItem(string itemName) { }

    public void EquipWeaponInOtherHand(ItemState item)
    {
        /*var otherWeaponCell = CurrentInteractingItemCellContainer.transform.parent.GetComponentsInChildren<WeaponSetItemCellContainer>()
            .Where(container => container != CurrentInteractingItemCellContainer)
            .First();
        otherWeaponCell.SwapAndPlaceItem(CurrentInteractingItemCellContainer);*/
    }

    public bool TryEquipInventoryItem(ItemSlot previousItemSlot)
    {
        if (previousItemSlot.BaseItemState is not EquipmentState)
        {
            return false;
        }
        switch (previousItemSlot.BaseItemState)
        {
            case WeaponState:
                var freeWeaponCells = weaponItems.Where(weaponData => weaponData.ItemState == null).ToList();
                if (freeWeaponCells.Count > 1)
                {
                    var rightWeaponCell = freeWeaponCells.Where(cell => cell.WeaponHandPosition == WeaponHandPosition.Right).First();
                    rightWeaponCell.TryPlaceOrSwapItem(previousItemSlot);
                    return true;
                }
                else if (freeWeaponCells.Count == 1)
                {
                    freeWeaponCells[0].TryPlaceOrSwapItem(previousItemSlot);
                    return true;
                }
                goto default;

            case ClothesState clothes:
                var freeClothesCell = clothesItems.Where(clothesData => clothesData.ItemState == null && clothesData.ClothesType == (clothes.BaseParams as ClothesData).ClothesType).FirstOrDefault();
                if (freeClothesCell != null)
                {
                    freeClothesCell.TryPlaceOrSwapItem(previousItemSlot);
                    return true;
                }
                goto default;

            default:
                return false;
        }
    }

    public void TakeEquipmentOff(ItemState item)
    {
        //(CurrentInteractingItemCellContainer as PlayerSetItemCellContainer).RemoveAttachedItem();       
    }

    private bool TryAddStackableItem(StackableItemState stackableItem)
    {
        var inventoryItemDatas = inventoryItems.Select(item => item as ItemSlot).Concat(quickAccessItems.Select(item => item as ItemSlot));
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

    private bool TryEquipOrAddItem(EquipmentState equipment)
    {
        switch (equipment)
        {
            case WeaponState weapon:
                var freeWeaponCells = weaponItems.Where(weaponData => weaponData.ItemState == null).ToList();
                if (freeWeaponCells.Count > 1)
                {
                    var rightWeaponCell = freeWeaponCells.Where(cell => cell.WeaponHandPosition == WeaponHandPosition.Right).First();
                    rightWeaponCell.ItemState = weapon;
                    return true;
                }
                else if (freeWeaponCells.Count == 1)
                {
                    freeWeaponCells[0].ItemState = weapon;
                    return true;
                }
                goto default;

            case ClothesState clothes:
                var freeClothesCell = clothesItems.Where(clothesData => clothesData.ItemState == null && clothesData.ClothesType == (clothes.BaseParams as ClothesData).ClothesType).FirstOrDefault();
                if (freeClothesCell != null)
                {
                    freeClothesCell.ItemState = clothes;
                    return true;                    
                }
                goto default;

            default:
                return TryAddItem(equipment);
        }
    }

    private bool TryAddItem(ItemState item)
    {
        return TryAddItemToCollection(quickAccessItems, item) || TryAddItemToCollection(inventoryItems, item);
    }

    private bool TryAddItemToCollection<T>(T[] itemDatas, ItemState item) where T : ItemSlot<ItemState>
    {
        var freeItemCells = itemDatas.Where(itemData => itemData.BaseItemState == null).ToList();
        if (freeItemCells.Count > 0)
        {
            freeItemCells[0].ItemState = item;
            return true;
        }
        return false;
    }

    private void AttachInventoryItemCellsToItems(InventorySectionView inventoryView)
    {
        AttachItemCellsToItems(inventoryView.InventorySubsectionView.GetCategoryView<MainInventoryCategoryView>().MainItemCells, inventoryItems);
        AttachItemCellsToItems(inventoryView.InventorySubsectionView.GetCategoryView<EquipmentCategoryView>().MainItemCells, quickAccessItems);
        AttachItemCellsToItems(inventoryView.InventorySubsectionView.GetCategoryView<EquipmentCategoryView>().WeaponCells, weaponItems);
        AttachItemCellsToItems(inventoryView.InventorySubsectionView.GetCategoryView<EquipmentCategoryView>().ClothesCells, clothesItems);
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
                    itemDescriptionsManager.CreateItemDescriptionView(inventoryItemData.BaseItemState, itemCell);
                }
            };
            itemCell.CellDeselected += () => itemDescriptionsManager.ClearItemDescriptionView();

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
}
