using System;
using Controls;
using EventBus;
using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemsCountSelectPanelPresenter : IDisposable
    {
        public event Action<int> ItemsCountSelected;
        public event Action AllItemsSelected;
        public event Action ItemsCountSelectionCanceled;

        private ItemsCountSelectPanelConfig _config;
        private ItemsCountSelectPanelView _view;  
        private SignalBus _signalBus;
        private int _selectedItemsCount;

        public ItemsCountSelectPanelPresenter(ItemsCountSelectPanelConfig config, ItemsCountSelectPanelView view, SignalBus signalBus)
        {
            _config = config;
            _view = view;
            _signalBus = signalBus;

            _view.ItemsCounterSlider.onValueChanged.AddListener(OnItemsCounterSliderValueChanged);
            _view.SelectItemsCountActionButton.ButtonComponent.onClick.AddListener(OnSelectItemsCountActionButtonPressed);
            _view.SelectAllItemsActionButton.ButtonComponent.onClick.AddListener(OnSelectAllItemsActionButtonPressed);
            _view.CancelActionButton.ButtonComponent.onClick.AddListener(OnCancelActionButtonPressed);

            _signalBus.Subscribe<PlayerMenuItemsCountSelectPanel_CancelPerformedSignal>(OnCancelItemsCountSelectionActionPerformed);
            _signalBus.Subscribe<PlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformedSignal>(OnChangeItemsCountActionPerformed);
            _signalBus.Subscribe<PlayerMenuItemsCountSelectPanel_SelectPerformedSignal>(OnSelectItemsActionPerformed);
            _signalBus.Subscribe<PlayerMenuItemsCountSelectPanel_SelectAllPerformedSignal>(OnSelectAllItemsActionPerformed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerMenuItemsCountSelectPanel_CancelPerformedSignal>(OnCancelItemsCountSelectionActionPerformed);
            _signalBus.Unsubscribe<PlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformedSignal>(OnChangeItemsCountActionPerformed);
            _signalBus.Unsubscribe<PlayerMenuItemsCountSelectPanel_SelectPerformedSignal>(OnSelectItemsActionPerformed);
            _signalBus.Unsubscribe<PlayerMenuItemsCountSelectPanel_SelectAllPerformedSignal>(OnSelectAllItemsActionPerformed);
        }

        public void StartItemsCountSelection(Sprite itemIcon, int minItemsCount, int maxItemsCount, bool isAllItemsSelectionEnabled = true)
        {
            _view.SetActive(true);
            _view.SetActionDescriptionText(_config.ActionDescriptionText.GetLocalizedString());
            _view.SetItemIcon(itemIcon);
            _view.SetItemsMinCountText(minItemsCount.ToString());
            _view.SetItemsMaxCountText(maxItemsCount.ToString());
            _view.ItemsCounterSlider.value = minItemsCount;
            _view.SelectAllItemsActionButton.SetActive(isAllItemsSelectionEnabled);

            _signalBus.Fire(new ActionMapRequestedSignal(_config.ActionMap));
        }

        private void FinishItemsCountSelection()
        {
            _view.SetActive(false);

            _signalBus.Fire(new PreviousActionMapRequestedSignal());
        }

        private void SelectItemsCount()
        {
            ItemsCountSelected?.Invoke(_selectedItemsCount);
            FinishItemsCountSelection();
        }

        private void SelectAllItems()
        {
            AllItemsSelected?.Invoke();
            FinishItemsCountSelection();
        }

        private void CancelItemsCountSelection()
        {
            ItemsCountSelectionCanceled?.Invoke();
            FinishItemsCountSelection();
        }

        private void OnItemsCounterSliderValueChanged(float value)
        {
            _selectedItemsCount = (int)value;
            _view.SetItemsCounterText(_selectedItemsCount.ToString());
        }

        private void OnSelectItemsCountActionButtonPressed() => SelectItemsCount();

        private void OnSelectAllItemsActionButtonPressed() => SelectAllItems();

        private void OnCancelActionButtonPressed() => CancelItemsCountSelection();

        private void OnChangeItemsCountActionPerformed(PlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformedSignal signal)
        {
            var inputValue = signal.Context.ReadValue<Vector2>().x;
            if (Mathf.Abs(inputValue) == 1)
            {
                _view.ItemsCounterSlider.value += (int)inputValue;
            }
        }

        private void OnSelectItemsActionPerformed(PlayerMenuItemsCountSelectPanel_SelectPerformedSignal signal) => SelectItemsCount();

        private void OnSelectAllItemsActionPerformed(PlayerMenuItemsCountSelectPanel_SelectAllPerformedSignal signal)
        {
            if (_view.SelectAllItemsActionButton.isActiveAndEnabled)
            {
                SelectAllItems();
            }
        }

        private void OnCancelItemsCountSelectionActionPerformed(PlayerMenuItemsCountSelectPanel_CancelPerformedSignal signal) => CancelItemsCountSelection();
    }
}