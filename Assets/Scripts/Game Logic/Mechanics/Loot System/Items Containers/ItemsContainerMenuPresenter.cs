using System;
using System.Linq;
using Controls;
using GameLogic.LootSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;
using Object = UnityEngine.Object;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsContainerMenuPresenter : IDisposable
    {
        private ItemsContainerMenuModel _model;
        private ItemsContainerMenuView _view;
        private ItemsContainerActionMap _actionMap;
        private InputManager _inputManager;
        private SignalBus _signalBus;

        public ItemsContainerMenuPresenter(ItemsContainerMenuView view, ItemsContainerActionMap actionMap, InputManager inputManager, SignalBus signalBus)
        {
            _view = view;
            _actionMap = actionMap;
            _inputManager = inputManager;
            _signalBus = signalBus;

            _inputManager.PlayerActions.HUDItemsContainer.CloseContainer.performed += OnCloseContainerActionPerformed;
            _inputManager.PlayerActions.HUDItemsContainer.Navigate.performed += OnNavigateActionPerformed;
            _inputManager.PlayerActions.HUDItemsContainer.Take.performed += OnPickItemActionPerformed;
            _inputManager.PlayerActions.HUDItemsContainer.TakeAll.performed += OnPickAllItemsActionPerformed;

            _signalBus.Subscribe<ObjectInteractionStartedSignal>(OnObjectInteractionStarted);
            _signalBus.Subscribe<ContainedItemPickedSignal>(OnContainedItemPicked);
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.HUDItemsContainer.CloseContainer.performed -= OnCloseContainerActionPerformed;
            _inputManager.PlayerActions.HUDItemsContainer.Navigate.performed -= OnNavigateActionPerformed;
            _inputManager.PlayerActions.HUDItemsContainer.Take.performed -= OnPickItemActionPerformed;
            _inputManager.PlayerActions.HUDItemsContainer.TakeAll.performed -= OnPickAllItemsActionPerformed;

            _signalBus.Unsubscribe<ObjectInteractionStartedSignal>(OnObjectInteractionStarted);
            _signalBus.Unsubscribe<ContainedItemPickedSignal>(OnContainedItemPicked);
        }

        private void OpenContainer(ItemsContainer itemsContainer)
        {
            var containedItemDatas = GetContainedItemDatas(itemsContainer.ContainedItems.ToArray());
            _model = new ItemsContainerMenuModel(itemsContainer, containedItemDatas);

            _view.SetActive(true);
            _view.SetContainerTitleText(_model.Container.BaseParams.Name.GetLocalizedString());


            _inputManager.SetActionMap(_actionMap);

            _signalBus.Subscribe<ControlsChangedSignal>(OnControlsChanged);
        }

        private void PickItem(ItemState item) => _signalBus.Fire(new ItemPickingRequestedSignal(item));

        private (ItemState item, ContainedItemView itemView)[] GetContainedItemDatas(ItemState[] containedItems)
        {
            var containedItemDatas = new (ItemState item, ContainedItemView itemView)[containedItems.Length];
            for (var i = 0; i < containedItemDatas.Length; i++)
            {
                var itemView = _view.GetContainedItemViewByIndex(i);
                itemView.SetActive(true);
                itemView.SetItemNameText(containedItems[i].Title.GetLocalizedString());
                itemView.SetItemDescriptionText(containedItems[i].Description.GetLocalizedString());
                itemView.SetItemIcon(containedItems[i].Icon);
                itemView.SetItemsCounterActive(containedItems[i] is StackableItemState);
                if (containedItems[i] is StackableItemState stackableItem)
                {
                    itemView.SetItemsCounterText($"x{stackableItem.Count.Value}");
                }

                itemView.ButtonComponent.onClick.AddListener(() => OnContainedItemViewPressed(itemView));
                itemView.PointerEnter += OnContainedItemViewPointerEnter;

                containedItemDatas[i] = (containedItems[i], itemView);
            }
            return containedItemDatas;
        }

        private void OnObjectInteractionStarted(ObjectInteractionStartedSignal signal)
        {
            if (signal.InteractiveObject is ItemsContainer itemsContainer)
            {
                OpenContainer(itemsContainer);
            }
        }

        private void OnContainedItemViewPressed(ContainedItemView containedItemView)
        {

        }

        private void OnContainedItemViewPointerEnter(PointerEventData eventData, ContainedItemView containedItemView)
        {

        }

        private void OnCloseContainerActionPerformed(InputAction.CallbackContext context)
        {
            _view.DisableAllContainedItemViews();
            _view.SetActive(false);

            if (_model.Container.ContainedItems.Count == 0)
            {
                _model.Container.SetFilledContainerEffectActive(false);
                Object.Destroy(_model.Container);
            }

            _signalBus.Unsubscribe<ControlsChangedSignal>(OnControlsChanged);
            _inputManager.SetPreviousActionMap();
        }

        private void OnNavigateActionPerformed(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>().y;
            if (Mathf.Abs(inputValue) == 1)
            {
                _model.SelectedItemNumber += (int)inputValue;
            }
        }

        private void OnSelectedItemNumberChanged(int selectedItemNumber)
        {
            var containedItemView = _view.GetContainedItemViewByIndex(selectedItemNumber - 1);
            containedItemView.ButtonComponent.Select();

            _view.SetScrollRectVerticalNormalizedPosition(1 - (float)selectedItemNumber / _model.ContainedItems.Length);
        }

        private void OnPickItemActionPerformed(InputAction.CallbackContext context) => PickItem(_model.ContainedItems[_model.SelectedItemNumber].item);

        private void OnPickAllItemsActionPerformed(InputAction.CallbackContext context)
        {
            foreach (var containedItemData in _model.ContainedItems)
            {
                PickItem(containedItemData.item);
            }
        }

        private void OnContainedItemPicked(ContainedItemPickedSignal signal)
        {
            var containedItemInfo = _model.ContainedItems.First(containedItem => containedItem.item == signal.Item);
            containedItemInfo.itemView.SetActive(false);
            containedItemInfo = (null, null);

            _model.SelectedItemNumber--;
        }

        private void OnControlsChanged() { }
    }
}