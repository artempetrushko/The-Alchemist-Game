using System;
using Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic.PlayerMenu
{
    public class ItemsCountSelectPanelPresenter : IDisposable
    {
        public event Action<int> ItemsCountSelected;
        public event Action AllItemsSelected;
        public event Action ItemsCountSelectionCanceled;

        private ItemsCountSelectPanelConfig _config;
        private ItemsCountSelectPanelView _view;
        private InputManager _inputManager;   
        private int _selectedItemsCount;

        public ItemsCountSelectPanelPresenter(ItemsCountSelectPanelConfig config, ItemsCountSelectPanelView view, InputManager inputManager)
        {
            _config = config;
            _view = view;
            _inputManager = inputManager;

            _view.ItemsCounterSlider.onValueChanged.AddListener(OnItemsCounterSliderValueChanged);
            _view.SelectItemsCountActionButton.ButtonComponent.onClick.AddListener(OnSelectItemsCountActionButtonPressed);
            _view.SelectAllItemsActionButton.ButtonComponent.onClick.AddListener(OnSelectAllItemsActionButtonPressed);
            _view.CancelActionButton.ButtonComponent.onClick.AddListener(OnCancelActionButtonPressed);

            _inputManager.PlayerActions.PlayerMenuItemsCountSelectPanel.ChangeItemsCount.performed += OnChangeItemsCountActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsCountSelectPanel.Select.performed += OnSelectItemsActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsCountSelectPanel.SelectAll.performed += OnSelectAllItemsActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsCountSelectPanel.Cancel.performed += OnCancelItemsCountSelectionActionPerformed;
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.PlayerMenuItemsCountSelectPanel.ChangeItemsCount.performed -= OnChangeItemsCountActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsCountSelectPanel.Select.performed -= OnSelectItemsActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsCountSelectPanel.SelectAll.performed -= OnSelectAllItemsActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsCountSelectPanel.Cancel.performed -= OnCancelItemsCountSelectionActionPerformed;
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

            _inputManager.SetActionMap(_config.ActionMap);
        }

        private void FinishItemsCountSelection()
        {
            _view.SetActive(false);

            _inputManager.SetPreviousActionMap();
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

        private void OnChangeItemsCountActionPerformed(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>().x;
            if (Mathf.Abs(inputValue) == 1)
            {
                _view.ItemsCounterSlider.value += (int)inputValue;
            }
        }

        private void OnSelectItemsActionPerformed(InputAction.CallbackContext context) => SelectItemsCount();

        private void OnSelectAllItemsActionPerformed(InputAction.CallbackContext context)
        {
            if (_view.SelectAllItemsActionButton.isActiveAndEnabled)
            {
                SelectAllItems();
            }
        }

        private void OnCancelItemsCountSelectionActionPerformed(InputAction.CallbackContext context) => CancelItemsCountSelection();
    }
}