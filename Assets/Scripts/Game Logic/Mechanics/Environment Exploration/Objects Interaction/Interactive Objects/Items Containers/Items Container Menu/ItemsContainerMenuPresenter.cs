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

            _inputManager.PlayerActions.ItemsContainerMenu.CloseContainer.performed += OnCloseContainerActionPerformed;
            _inputManager.PlayerActions.ItemsContainerMenu.Navigate.performed += OnNavigateActionPerformed;
            _inputManager.PlayerActions.ItemsContainerMenu.PickItem.performed += OnPickItemActionPerformed;
            _inputManager.PlayerActions.ItemsContainerMenu.PickAllItems.performed += OnPickAllItemsActionPerformed;

            _signalBus.Subscribe<ContainedItemPickedSignal>(OnContainedItemPicked);
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.ItemsContainerMenu.CloseContainer.performed -= OnCloseContainerActionPerformed;
            _inputManager.PlayerActions.ItemsContainerMenu.Navigate.performed -= OnNavigateActionPerformed;
            _inputManager.PlayerActions.ItemsContainerMenu.PickItem.performed -= OnPickItemActionPerformed;
            _inputManager.PlayerActions.ItemsContainerMenu.PickAllItems.performed -= OnPickAllItemsActionPerformed;

            _signalBus.Unsubscribe<ContainedItemPickedSignal>(OnContainedItemPicked);
        }

        public void Show(ItemsContainer itemsContainer)
        {
            var containedItemDatas = GetContainedItemDatas(itemsContainer.ContainedItems.ToArray());
            _model = new ItemsContainerMenuModel(itemsContainer, containedItemDatas);

            _view.SetActive(true);
            _view.SetContainerTitleText(_model.Container.Name.GetLocalizedString());


            _inputManager.SetActionMap(_actionMap);

            _signalBus.Subscribe<ControlsChangedSignal>(OnControlsChanged);
        }

        private void PickItem(Item item) => _signalBus.Fire(new ItemPickingRequestedSignal(item));

        private (Item item, ContainedItemView itemView)[] GetContainedItemDatas(Item[] containedItems)
        {
            var containedItemDatas = new (Item item, ContainedItemView itemView)[containedItems.Length];
            for (var i = 0; i < containedItemDatas.Length; i++)
            {
                var itemView = _view.GetContainedItemViewByIndex(i);
                itemView.SetActive(true);
                itemView.SetItemNameText(containedItems[i].Title.GetLocalizedString());
                itemView.SetItemDescriptionText(containedItems[i].Description.GetLocalizedString());
                itemView.SetItemIcon(containedItems[i].Icon);
                itemView.SetItemsCounterActive(containedItems[i] is StackableItem);
                if (containedItems[i] is StackableItem stackableItem)
                {
                    itemView.SetItemsCounterText($"x{stackableItem.Count.Value}");
                }

                itemView.ButtonComponent.onClick.AddListener(() => OnContainedItemViewPressed(itemView));
                itemView.PointerEnter += OnContainedItemViewPointerEnter;

                containedItemDatas[i] = (containedItems[i], itemView);
            }
            return containedItemDatas;
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