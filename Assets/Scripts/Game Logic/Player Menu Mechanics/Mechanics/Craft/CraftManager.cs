using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CraftManager : PlayerMenuMechanicsManager
{
    [SerializeField]
    private RecipeData[] availableCraftRecipes;
    [SerializeField]
    private float craftingTimeInSecond;
    [SerializeField]
    private float craftingStepTimeInSecond;
    [Space, SerializeField]
    private CraftInventoryCategoriesManager inventoryCategoriesManager;
    [Space, SerializeField]
    private InventoryManager inventoryManager;

    private CraftSectionView craftSection;
    private RecipeData currentRecipe;
    private RecipeVariant currentRecipeVariant;
    private int? currentExtractedEnergyCount;

    private bool isCreationAvailable;
    private bool isAllIngredientsPlaced;
    private bool isCraftingStarted;

    private SimpleItemSlot[] ingredients;
    private SimpleItemSlot[] energyHolders;

    public RecipeData CurrentRecipe
    {
        get => currentRecipe;
        set
        {
            if (currentRecipe != value)
            {
                if (currentRecipe != null)
                {
                    craftSection.ItemCreationSectionView.ClearCraftTemplate();
                    CurrentExtractedEnergyCount = null;
                }
                currentRecipe = value;
                if (currentRecipe != null)
                {
                    craftSection.ItemCreationSectionView.CreateNewCraftTemplate(currentRecipe.RecipeIcon, currentRecipe.IngredientCellsTemplate);
                    CurrentExtractedEnergyCount = 0;
                    ingredients = CreateCraftItemSlots(craftSection.ItemCreationSectionView.IngredientCells, UpdateRequiredItemsProgress);
                }                           
            }           
        }
    }
    public bool IsCreationAvailable
    {
        get => isCreationAvailable;
        private set
        {
            isCreationAvailable = value;
            craftSection.CraftingProcessStateView.SetCreationAvailabilityState(isCreationAvailable, isCreationAvailable 
                ? "Готово к созданию!"
                : !IsAllIngredientsPlaced 
                    ? "Недостаточно ингредиентов!"
                    : "Недостаточно энергии!");         
        }
    }
    private bool IsAllIngredientsPlaced
    {
        get => isAllIngredientsPlaced;
        set
        {
            if (isAllIngredientsPlaced != value)
            {
                isAllIngredientsPlaced = value;
                IsCreationAvailable = isAllIngredientsPlaced && CurrentExtractedEnergyCount >= CurrentRecipe.RequiredEnergyCount;
            }
        }
    }
    private int? CurrentExtractedEnergyCount
    {
        get => currentExtractedEnergyCount;
        set
        {
            currentExtractedEnergyCount = value;
            if (currentExtractedEnergyCount != null)
            {
                craftSection.CraftingProcessStateView.SetExtractedEnergyCountInfo("Выделено энергии:", CurrentExtractedEnergyCount.Value, currentRecipe.RequiredEnergyCount);
                IsCreationAvailable = IsAllIngredientsPlaced && currentExtractedEnergyCount >= CurrentRecipe.RequiredEnergyCount;
            }
        }
    }

    public override void InitializeLinkedView(PlayerMenuSectionView mechanicsLinkedView)
    {
        if (mechanicsLinkedView is CraftSectionView craftSectionView)
        {
            craftSection = craftSectionView;
            craftSection.RecipesSectionView.CreateRecipeCategories(availableCraftRecipes, (recipe) => CurrentRecipe = recipe);
            craftSection.ItemCreationSectionView.AddEventTriggerListener(EventTriggerType.PointerDown, (eventData) => StartCraftingProcess());
            craftSection.ItemCreationSectionView.AddEventTriggerListener(EventTriggerType.PointerUp, (eventData) => StopCraftingProcess());
            inventoryCategoriesManager.Initialize(craftSection.InventorySubsectionView, craftSection.SectionNavigation);
            energyHolders = CreateCraftItemSlots(craftSection.ItemCreationSectionView.EnergyCells, UpdateExtractedEnergyCount);
        }
    }

    public void UpdateRequiredItemsProgress()
    {
        if (ingredients.All(ingredient => ingredient.ItemState == null))
        {
            foreach (var ingredient in ingredients)
            {
                (ingredient.CellView as IngredientCellView).UpdateItemsCounter(0, null);
            }
            return;
        }

        var containedItemStates = ingredients.Select(ingredient => ingredient.ItemState).ToList();
        currentRecipeVariant = currentRecipe.TryGetMatchingRecipeVariant(containedItemStates);
        SetTemplateCellViewCounterInfos();
        if (currentRecipeVariant != null)
        {
            IsAllIngredientsPlaced = currentRecipeVariant.CheckCraftingAvailability(containedItemStates);
        }
    }

    public void StartCraftingProcess(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCraftingProcess();
        }
        else if (context.canceled)
        {
            StopCraftingProcess();
        }
    }

    private void StartCraftingProcess()
    {
        if (isCreationAvailable && !isCraftingStarted)
        {
            isCraftingStarted = true;
            StartCoroutine(CraftNewItem_COR());
        }
    }

    private void StopCraftingProcess()
    {
        isCraftingStarted = false;
        craftSection.ItemCreationSectionView.HideCraftProgressBar();
    }

    private IEnumerator CraftNewItem_COR()
    {
        var craftProgressBarFillAmount = 0f;
        var craftProgressFillStep = 1f / (craftingTimeInSecond / craftingStepTimeInSecond);
        while (isCraftingStarted && craftProgressBarFillAmount < 1)
        {
            craftProgressBarFillAmount += craftProgressFillStep;
            craftSection.ItemCreationSectionView.FillCraftProgressBar(craftProgressBarFillAmount);
            yield return new WaitForSecondsRealtime(craftingStepTimeInSecond);
        }
        if (isCraftingStarted)
        {
            inventoryManager.AddNewItemState(currentRecipeVariant.ResultItem.GetResultItemState());
            UpdateIngredientsStates();
            UpdateEnergyHoldersStates();
            isCraftingStarted = false;
        }        
    }

    private SimpleItemSlot[] CreateCraftItemSlots(ItemCellView[] cellViews, Action itemStateChangedAction)
    {
        return cellViews
            .Select(cellView =>
            {
                var itemSlot = new SimpleItemSlot
                {
                   CellView = cellView
                };
                itemSlot.CellView.AddEventTriggerListener(EventTriggerType.Drop, (eventData) => itemSlot.TryPlaceOrSwapItem(ItemViewDraggingModule.DraggingItem.LinkedItem.LinkedItemSlot));
                itemSlot.ItemStateChanged += itemStateChangedAction;
                return itemSlot;
            })
            .ToArray();
    }

    private void SetTemplateCellViewCounterInfos()
    {
        for (var i = 0; i < ingredients.Length; i++)
        {
            var currentItemsCount = ingredients[i].ItemState switch
            {
                StackableItemState stackableItem => stackableItem.ItemsCount,
                SingleItemState => 1,
                null => 0,
            };
            int? requiredItemsCount = currentRecipeVariant?.Ingredients[i].Count;
            (ingredients[i].CellView as IngredientCellView).UpdateItemsCounter(currentItemsCount, requiredItemsCount);
        }
    }

    private void UpdateIngredientsStates()
    {
        for (var i = 0; i < ingredients.Length; i++)
        {
            var usedItem = ingredients[i];
            switch (usedItem.ItemState)
            {
                case StackableItemState stackableItem:
                    var spentItemsCount = currentRecipeVariant.Ingredients[i].Count;
                    if (stackableItem.ItemsCount == spentItemsCount)
                    {
                        usedItem.ClearItemState();
                    }
                    else
                    {
                        stackableItem.ItemsCount -= spentItemsCount;
                    }
                    break;

                case SingleItemState:
                    usedItem.ClearItemState();
                    break; 
            }
        }
        UpdateRequiredItemsProgress();
    }

    private void UpdateEnergyHoldersStates()
    {
        var requiredEnergyCount = currentRecipe.RequiredEnergyCount;
        foreach (var energyHolder in energyHolders)
        {
            switch (energyHolder.ItemState)
            {
                case StackableItemState stackableItem:
                    if (stackableItem.TotalContainedEnergyCount <= requiredEnergyCount)
                    {
                        requiredEnergyCount -= stackableItem.TotalContainedEnergyCount;
                        energyHolder.ClearItemState();
                    }
                    else
                    {
                        stackableItem.ItemsCount -= (int)Mathf.Ceil((float)requiredEnergyCount / stackableItem.ContainedEnergyCount);
                        if (stackableItem.ItemsCount == 0)
                        {
                            energyHolder.ClearItemState();
                        }
                        requiredEnergyCount = 0;                        
                    }
                    break;

                case SingleItemState:
                    requiredEnergyCount -= energyHolder.ItemState.ContainedEnergyCount;
                    energyHolder.ClearItemState();
                    break;
            }
            if (requiredEnergyCount <= 0)
            {
                break;
            }
        }
        UpdateExtractedEnergyCount();
    }

    private void UpdateExtractedEnergyCount()
    {
        CurrentExtractedEnergyCount = energyHolders
            .Select(energyHolder => energyHolder.ItemState switch
            {
                StackableItemState stackableItem => stackableItem.TotalContainedEnergyCount,
                _ => energyHolder.ItemState.ContainedEnergyCount
            })
            .Sum();
    }
}
