using System;
using System.Linq;
using Controls;
using EventBus;
using GameLogic.LootSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Object = UnityEngine.Object;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsContainerMenuPresenter : IDisposable
    {
        private ItemsContainerMenuModel _model;
        private ItemsContainerMenuView _view;
        private ItemsContainerActionMap _actionMap;
        private SignalBus _signalBus;

        public ItemsContainerMenuPresenter(ItemsContainerMenuView view, ItemsContainerActionMap actionMap, SignalBus signalBus)
        {
            _view = view;
            _actionMap = actionMap;
            _signalBus = signalBus;

            _signalBus.Subscribe<ItemsContainerMenu_CloseContainerPerformedSignal>(OnCloseContainerActionPerformed);
            _signalBus.Subscribe<ItemsContainerMenu_NavigatePerformedSignal>(OnNavigateActionPerformed);
            _signalBus.Subscribe<ItemsContainerMenu_PickItemPerformedSignal>(OnPickItemActionPerformed);
            _signalBus.Subscribe<ItemsContainerMenu_PickAllItemsPerformedSignal>(OnPickAllItemsActionPerformed);

            _signalBus.Subscribe<ContainedItemPickedSignal>(OnContainedItemPicked);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ItemsContainerMenu_CloseContainerPerformedSignal>(OnCloseContainerActionPerformed);
            _signalBus.Unsubscribe<ItemsContainerMenu_NavigatePerformedSignal>(OnNavigateActionPerformed);
            _signalBus.Unsubscribe<ItemsContainerMenu_PickItemPerformedSignal>(OnPickItemActionPerformed);
            _signalBus.Unsubscribe<ItemsContainerMenu_PickAllItemsPerformedSignal>(OnPickAllItemsActionPerformed);

            _signalBus.Unsubscribe<ContainedItemPickedSignal>(OnContainedItemPicked);
        }

        public void Show(ItemsContainer itemsContainer)
        {
            var containedItemDatas = GetContainedItemDatas(itemsContainer.ContainedItems.ToArray());
            _model = new ItemsContainerMenuModel(itemsContainer, containedItemDatas);

            _view.SetActive(true);
            _view.SetContainerTitleText(_model.Container.Name.GetLocalizedString());

            _signalBus.Fire(new ActionMapRequestedSignal(_actionMap));

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

        private void OnCloseContainerActionPerformed(ItemsContainerMenu_CloseContainerPerformedSignal signal)
        {
            _view.DisableAllContainedItemViews();
            _view.SetActive(false);

            if (_model.Container.ContainedItems.Count == 0)
            {
                _model.Container.SetFilledContainerEffectActive(false);
                Object.Destroy(_model.Container);
            }

            _signalBus.Unsubscribe<ControlsChangedSignal>(OnControlsChanged);
            _signalBus.Fire(new PreviousActionMapRequestedSignal());
        }

        private void OnNavigateActionPerformed(ItemsContainerMenu_NavigatePerformedSignal signal)
        {
            var inputValue = signal.Context.ReadValue<Vector2>().y;
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

        private void OnPickItemActionPerformed(ItemsContainerMenu_PickItemPerformedSignal signal) => PickItem(_model.ContainedItems[_model.SelectedItemNumber].item);

        private void OnPickAllItemsActionPerformed(ItemsContainerMenu_PickAllItemsPerformedSignal signal)
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