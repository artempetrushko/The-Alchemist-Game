using System;
using System.Linq;
using Controls;
using Cysharp.Threading.Tasks;
using EventBus;
using GameLogic.LootSystem;
using ModestTree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace GameLogic.PlayerMenu.Craft
{
    public class CraftPresenter : IDisposable
    {
        private CraftConfig _craftConfig;
        private CraftModel _craftModel;
        private CraftView _craftView;
        private RecipesMenuPresenter _recipesMenuPresenter;
        private ItemCraftingStatusPanelConfig _itemCraftingStatusPanelConfig;
        private SignalBus _signalBus;
        private bool _isCraftingStarted = false;

        public CraftPresenter(CraftView craftView, SignalBus signalBus)
        {
            _craftView = craftView;
            _signalBus = signalBus;

            _craftModel = new CraftModel()
            {
                EnergySlots = new EnergySlots(_craftView.ItemCraftingSectionView.EnergySlotViews, _signalBus)
            };

            _craftView.ItemCraftingSectionView.ItemCraftingPlaceView.PointerDown += OnItemCraftingPlacePointerDown;
            _craftView.ItemCraftingSectionView.ItemCraftingPlaceView.PointerUp += OnItemCraftingPlacePointerUp;

            _recipesMenuPresenter.RecipeSelected += OnRecipeSelected;

            _signalBus.Subscribe<EnergySlotItemChangedSignal>(OnEnergySlotItemChanged);
            _signalBus.Subscribe<IngredientSlotItemChangedSignal>(OnIngredientSlotItemChanged);

            _signalBus.Subscribe<PlayerMenuCraftSection_CreateItemPerformedSignal>(OnCreateItemActionPerformed);
        }

        public void Dispose()
        {
            _craftView.ItemCraftingSectionView.ItemCraftingPlaceView.PointerDown -= OnItemCraftingPlacePointerDown;
            _craftView.ItemCraftingSectionView.ItemCraftingPlaceView.PointerUp -= OnItemCraftingPlacePointerUp;

            _recipesMenuPresenter.RecipeSelected -= OnRecipeSelected;

            _signalBus.Unsubscribe<EnergySlotItemChangedSignal>(OnEnergySlotItemChanged);
            _signalBus.Unsubscribe<IngredientSlotItemChangedSignal>(OnIngredientSlotItemChanged);

            _signalBus.Unsubscribe<PlayerMenuCraftSection_CreateItemPerformedSignal>(OnCreateItemActionPerformed);
        }

        public void UpdateRequiredItemsProgress()
        {
            var containedItemStates = _craftModel.IngredientSlots.Slots
                .Select(ingredient => ingredient.ContainedItem)
                .ToArray();
            _craftModel.CurrentRecipeVariant = TryGetMatchingRecipeVariant(containedItemStates); 
            if (_craftModel.CurrentRecipeVariant != null)
            {
                _craftModel.IsAllIngredientsPlaced = CheckCraftingAvailability(_craftModel.CurrentRecipeVariant, containedItemStates);
                for (var i = 0; i < _craftModel.IngredientSlots.Slots.Length; i++)
                {
                    _craftModel.IngredientSlots.Slots[i].UpdateRequiredItemsCounter(_craftModel.CurrentRecipeVariant.Ingredients[i].Count);
                }
            }
            else
            {
                for (var i = 0; i < _craftModel.IngredientSlots.Slots.Length; i++)
                {
                    _craftModel.IngredientSlots.Slots[i].UpdateRequiredItemsCounter(null);
                }
            }
        }

        private void UpdateCraftAvailabilityStatus()
        {
            var newCraftingAvailabilityStatus = CraftingAvailabilityStatus.Available;
            if (!_craftModel.IsAllIngredientsPlaced)
            {
                newCraftingAvailabilityStatus = CraftingAvailabilityStatus.NoIngredientsEnough;
            }
            else if (_craftModel.CurrentExtractedEnergyCount < _craftModel.CurrentRecipe.RequiredEnergyCount)
            {
                newCraftingAvailabilityStatus = CraftingAvailabilityStatus.NoEnergyEnough;
            }

            if (_craftModel.CraftingAvailabilityStatus != newCraftingAvailabilityStatus)
            {
                _craftModel.CraftingAvailabilityStatus = newCraftingAvailabilityStatus;
                var craftingAvailabilityStatusTextConfig = _itemCraftingStatusPanelConfig.CraftingAvailabilityStatusTextsConfigs.FirstOrDefault(config => config.Status == _craftModel.CraftingAvailabilityStatus);
                if (craftingAvailabilityStatusTextConfig != null)
                {
                    _craftView.ItemCraftingStatusPanelView.SetCraftingAvailabilityText(craftingAvailabilityStatusTextConfig.Text.GetLocalizedString());
                    _craftView.ItemCraftingStatusPanelView.SetCraftingAvailabilityTextColor(_craftModel.CraftingAvailabilityStatus == CraftingAvailabilityStatus.Available
                        ? _itemCraftingStatusPanelConfig.CraftingAvailabilityColor
                        : _itemCraftingStatusPanelConfig.CraftingUnavailabilityColor);
                }
            }
        }

        private void StartCraftingProcess()
        {
            if (_craftModel.CraftingAvailabilityStatus == CraftingAvailabilityStatus.Available && !_isCraftingStarted)
            {
                _isCraftingStarted = true;
                CraftNewItemAsync().Forget();
            }
        }

        private void StopCraftingProcess()
        {
            _isCraftingStarted = false;
            _craftView.ItemCraftingSectionView.SetCraftProgressBarFillAmount(0f);
            _craftView.ItemCraftingSectionView.SetCraftProgressBarActive(false);
        }

        private async UniTask CraftNewItemAsync()
        {
            var craftProgressBarFillAmount = 0f;
            var craftProgressFillStep = 1f / _craftConfig.CraftingTimeInSeconds * _craftConfig.CraftingStepTimeInSeconds;

            _craftView.ItemCraftingSectionView.SetCraftProgressBarActive(true);
            while (_isCraftingStarted && craftProgressBarFillAmount < 1)
            {
                craftProgressBarFillAmount += craftProgressFillStep;
                _craftView.ItemCraftingSectionView.SetCraftProgressBarFillAmount(craftProgressBarFillAmount);
                await UniTask.WaitForSeconds(_craftConfig.CraftingStepTimeInSeconds);
            }

            if (_isCraftingStarted)
            {
                var craftedItem = GetSelectedRecipeResultItem();
                _signalBus.Fire(new ItemCraftedSignal(craftedItem));

                SpendIngredients();
                SpendEnergyContainingItems();

                _isCraftingStarted = false;
            }
        }

        private void SpendIngredients()
        {
            for (var i = 0; i < _craftModel.IngredientSlots.Slots.Length; i++)
            {
                var ingredientSlot = _craftModel.IngredientSlots.Slots[i];
                switch (ingredientSlot.ContainedItem)
                {
                    case StackableItem stackableItem:
                        var spentItemsCount = _craftModel.CurrentRecipeVariant.Ingredients[i].Count;
                        if (stackableItem.Count.Value > spentItemsCount)
                        {
                            stackableItem.Count.Value -= spentItemsCount;
                            break;
                        }
                        goto default;

                    default:
                        ingredientSlot.Clear();
                        break;
                }
            }
            UpdateRequiredItemsProgress();
        }

        private void SpendEnergyContainingItems()
        {
            var requiredEnergyCount = _craftModel.CurrentRecipe.RequiredEnergyCount;
            foreach (var energySlot in _craftModel.EnergySlots.Slots)
            {
                switch (energySlot.ContainedItem)
                {
                    case StackableItem stackableItem:
                        if (stackableItem.TotalContainedEnergyCount <= requiredEnergyCount)
                        {
                            requiredEnergyCount -= stackableItem.TotalContainedEnergyCount;
                            energySlot.Clear();
                        }
                        else
                        {
                            stackableItem.Count.Value -= (int)Mathf.Ceil((float)requiredEnergyCount / stackableItem.ContainedEnergyCount.Value);
                            if (stackableItem.Count.Value == 0)
                            {
                                energySlot.Clear();
                            }
                            requiredEnergyCount = 0;
                        }
                        break;

                    default:
                        requiredEnergyCount -= energySlot.ContainedItem.ContainedEnergyCount.Value;
                        energySlot.Clear();
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
            _craftModel.CurrentExtractedEnergyCount = _craftModel.EnergySlots.Slots
                .Sum(energyHolder => energyHolder.ContainedItem switch
                {
                    StackableItem stackableItem => stackableItem.TotalContainedEnergyCount,
                    _ => energyHolder.ContainedItem.ContainedEnergyCount.Value
                });
            _craftView.ItemCraftingStatusPanelView.SetEnergyCounterText($"{_itemCraftingStatusPanelConfig.EnergyCounterLabelText.GetLocalizedString()}: {_craftModel.CurrentExtractedEnergyCount}/{_craftModel.CurrentRecipe.RequiredEnergyCount}");

            UpdateCraftAvailabilityStatus();
        }







        public RecipeVariant TryGetMatchingRecipeVariant(Item[] selectedItems)
        {
            return _craftModel.CurrentRecipe.RecipeVariants.FirstOrDefault(variant => CheckIngredientsRequirements(variant, selectedItems));
        }

        public bool CheckCraftingAvailability(RecipeVariant recipeVariant, Item[] selectedIngredients) => CheckIngredientsRequirements(recipeVariant, selectedIngredients, true);

        private bool CheckIngredientsRequirements(RecipeVariant recipeVariant, Item[] selectedIngredients, bool isCountMatchingRequired = false)
        {
            return selectedIngredients.All(item =>
            {
                if (item == null)
                {
                    return false;
                }
                var accordingIngredient = recipeVariant.Ingredients[selectedIngredients.IndexOf(item)];
                return CheckSelectedItemMatching(accordingIngredient, item, isCountMatchingRequired);
            });
        }

        private bool CheckSelectedItemMatching(RecipeItem requiredIngredient, Item selectedIngredient, bool isCountMatchingRequired = false)
        {
            if (isCountMatchingRequired && selectedIngredient is StackableItem stackableIngredient)
            {
                return stackableIngredient.Id == requiredIngredient.ItemConfig.Id && stackableIngredient.Count.Value >= requiredIngredient.Count;
            }
            return selectedIngredient.Id == requiredIngredient.ItemConfig.Id;
        }

        public Item GetSelectedRecipeResultItem()
        {
            var selectedRecipeResultItemConfig = _craftModel.CurrentRecipeVariant.ResultItem;
            var resultItem = selectedRecipeResultItemConfig.ItemConfig.CreateItem();
            if (resultItem is StackableItem stackableItem)
            {
                stackableItem.Count.Value = selectedRecipeResultItemConfig.Count;
            }
            return resultItem;
        }

        private void OnRecipeSelected(Recipe selectedRecipe)
        {
            if (_craftModel.CurrentRecipe != selectedRecipe)
            {
                if (_craftModel.CurrentRecipe != null)
                {
                    _craftModel.RecipeInfos.First(recipeInfo => recipeInfo.recipe == _craftModel.CurrentRecipe).ingredientSlotsTemplate.SetActive(false);
                }
                _craftModel.CurrentRecipe = selectedRecipe;
                if (_craftModel.CurrentRecipe != null)
                {
                    _craftView.ItemCraftingSectionView.ItemCraftingPlaceView.CreateIngredientSlotsTemplate(_craftModel.CurrentRecipe.IngredientCellsTemplate);
                    _craftView.ItemCraftingSectionView.ItemCraftingPlaceView.SetCraftingItemIconActive(true);
                    _craftView.ItemCraftingSectionView.ItemCraftingPlaceView.SetCraftingItemIcon(_craftModel.CurrentRecipe.Icon);

                    var ingredientSlotViews = _craftModel.RecipeInfos.First(recipeInfo => recipeInfo.recipe == selectedRecipe).ingredientSlotsTemplate.IngredientSlotViews;
                    _craftModel.IngredientSlots = new IngredientSlots(ingredientSlotViews, _signalBus);
                }
            }
        }

        private void OnEnergySlotItemChanged(EnergySlotItemChangedSignal signal) => UpdateExtractedEnergyCount();

        private void OnIngredientSlotItemChanged(IngredientSlotItemChangedSignal signal)
        {

        }

        private void OnItemCraftingPlacePointerDown(PointerEventData eventData) => StartCraftingProcess();

        private void OnItemCraftingPlacePointerUp(PointerEventData eventData) => StopCraftingProcess();

        private void OnCreateItemActionPerformed(PlayerMenuCraftSection_CreateItemPerformedSignal signal) => StartCraftingProcess();

        private void OnCreateItemActionCanceled(InputAction.CallbackContext context) => StopCraftingProcess();
    }
}