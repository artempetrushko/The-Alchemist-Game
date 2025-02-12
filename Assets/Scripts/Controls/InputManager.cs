using System;
using System.Linq;
using EventBus;
using UnityEngine.InputSystem;
using Zenject;

namespace Controls
{
    public class InputManager : IDisposable
    {
        private readonly InputConfig _inputConfig;
        private readonly PlayerInput _playerInput;
        private readonly SignalBus _signalBus;
        private readonly PlayerInputActions _playerActions;

        private PlayerInputActionMap _currentActionMap;
        private PlayerInputActionMap _previousActionMap;

        public InputManager(PlayerInput playerInput, InputConfig inputConfig, SignalBus signalBus)
        {
            _playerInput = playerInput;
            _inputConfig = inputConfig;
            _signalBus = signalBus;

            _playerActions = new PlayerInputActions();
            _currentActionMap = _inputConfig.DefaultActionMap;

            _playerInput.controlsChangedEvent.AddListener(OnControlsChanged);

            _playerActions.ItemsContainerMenu.CloseContainer.performed += OnItemsContainerMenu_CloseContainerPerformed;
            _playerActions.ItemsContainerMenu.Navigate.performed += OnItemsContainerMenu_NavigatePerformed;
            _playerActions.ItemsContainerMenu.PickAllItems.performed += OnItemsContainerMenu_PickAllItemsPerformed;
            _playerActions.ItemsContainerMenu.PickItem.performed += OnItemsContainerMenu_PickItemPerformed;           

            _playerActions.Player.Interact.performed += OnPlayer_InteractPerformed;
            _playerActions.Player.PickItem.performed += OnPlayer_PickItemPerformed;
            _playerActions.Player.SelectQuickAccessCell.performed += OnPlayer_SelectQuickAccessCellPerformed;
            _playerActions.Player.SelectNeighboringQuickAccessCell.performed += OnPlayer_SelectNeighboringQuickAccessCellPerformedSignal;
            _playerActions.Player.ShowPlayerMenu.performed += OnShowPlayerMenuPerformed;
            _playerActions.Player.ShowQuestProgress.performed += OnPlayer_ShowQuestProgressPerformed;

            _playerActions.PlayerMenuCraftSection.ClosePlayerMenu.performed += OnHidePlayerMenuPerformed;
            _playerActions.PlayerMenuCraftSection.CreateItem.performed += OnPlayerMenuCraftSection_CreateItemPerformed;           

            _playerActions.PlayerMenuInventorySection.NavigatePlayerMenu.performed += OnPlayerMenuInventorySection_NavigatePlayerMenuPerformed;
            _playerActions.PlayerMenuInventorySection.NavigateSection.performed += OnPlayerMenuInventorySection_NavigateSectionPerformed;
            _playerActions.PlayerMenuInventorySection.ClosePlayerMenu.performed += OnHidePlayerMenuPerformed;

            _playerActions.PlayerMenuItemsCountSelectPanel.Cancel.performed += OnPlayerMenuItemsCountSelectPanel_CancelPerformed;
            _playerActions.PlayerMenuItemsCountSelectPanel.ChangeItemsCount.performed += OnPlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformed;
            _playerActions.PlayerMenuItemsCountSelectPanel.Select.performed += OnPlayerMenuItemsCountSelectPanel_SelectPerformed;
            _playerActions.PlayerMenuItemsCountSelectPanel.SelectAll.performed += OnPlayerMenuItemsCountSelectPanel_SelectAllPerformed;    

            _playerActions.PlayerMenuItemsInteractionsPanel.CloseItemCellActionsMenu.performed += OnPlayerMenuItemsInteractionsPanel_CloseItemCellActionsMenuPerformed;
            _playerActions.PlayerMenuItemsInteractionsPanel.Navigate.performed += OnPlayerMenuItemsInteractionsPanel_NavigatePerformed;
            _playerActions.PlayerMenuItemsInteractionsPanel.Select.performed += OnPlayerMenuItemsInteractionsPanel_SelectPerformed;         
        }

        public void Dispose()
        {
            _playerActions.ItemsContainerMenu.CloseContainer.performed += OnItemsContainerMenu_CloseContainerPerformed;
            _playerActions.ItemsContainerMenu.Navigate.performed += OnItemsContainerMenu_NavigatePerformed;
            _playerActions.ItemsContainerMenu.PickAllItems.performed += OnItemsContainerMenu_PickAllItemsPerformed;
            _playerActions.ItemsContainerMenu.PickItem.performed += OnItemsContainerMenu_PickItemPerformed;

            _playerActions.Player.Interact.performed += OnPlayer_InteractPerformed;
            _playerActions.Player.PickItem.performed += OnPlayer_PickItemPerformed;
            _playerActions.Player.ShowPlayerMenu.performed += OnShowPlayerMenuPerformed;
            _playerActions.Player.ShowQuestProgress.performed += OnPlayer_ShowQuestProgressPerformed;

            _playerActions.PlayerMenuCraftSection.ClosePlayerMenu.performed += OnHidePlayerMenuPerformed;
            _playerActions.PlayerMenuCraftSection.CreateItem.performed += OnPlayerMenuCraftSection_CreateItemPerformed;

            _playerActions.PlayerMenuInventorySection.NavigatePlayerMenu.performed += OnPlayerMenuInventorySection_NavigatePlayerMenuPerformed;
            _playerActions.PlayerMenuInventorySection.NavigateSection.performed += OnPlayerMenuInventorySection_NavigateSectionPerformed;
            _playerActions.PlayerMenuInventorySection.ClosePlayerMenu.performed += OnHidePlayerMenuPerformed;

            _playerActions.PlayerMenuItemsInteractionsPanel.CloseItemCellActionsMenu.performed -= OnPlayerMenuItemsInteractionsPanel_CloseItemCellActionsMenuPerformed;
            _playerActions.PlayerMenuItemsInteractionsPanel.Navigate.performed -= OnPlayerMenuItemsInteractionsPanel_NavigatePerformed;
            _playerActions.PlayerMenuItemsInteractionsPanel.Select.performed -= OnPlayerMenuItemsInteractionsPanel_SelectPerformed;
        }

        public void SetActionMap(PlayerInputActionMap actionMap)
        {
            _previousActionMap = _currentActionMap;

            _currentActionMap = actionMap;
            _playerInput.SwitchCurrentActionMap(_currentActionMap.Name); 
        }

        public void SetPreviousActionMap()
        {
            //TODO: паттерн Memento
            //_currentActionMap = _previousActionMap;

            _playerInput.SwitchCurrentActionMap(_currentActionMap.Name);

            _previousActionMap = null;
        }

        public DetailedControlTip[] GetCurrentControlTips() => GetControlTips(_currentActionMap.GetActionInfos());

        public DetailedControlTip[] GetControlTips((string name, InputAction inputAction)[] actionInfos)
        {
            var controlTips = new DetailedControlTip[actionInfos.Length];
            for (var i = 0; i < controlTips.Length; i++)
            {
                if (_playerInput.currentControlScheme == _inputConfig.KeyboardMouseSchemeName)
                {
                    controlTips[i] = new DetailedControlTip(actionInfos[i].name, actionInfos[i].inputAction.controls[0].displayName);
                }
                else if (_playerInput.currentControlScheme == _inputConfig.GamepadSchemeName)
                {
                    var actionControl = actionInfos[i].inputAction.controls.First(x => x.device.name.Contains("Gamepad"));
                    var keyIcon = _inputConfig.GamepadButtonConfigs.First(config => config.DisplayNames.Contains(actionControl.displayName)).Icon;
                    controlTips[i] = new DetailedControlTip(actionInfos[i].name, actionInfos[i].inputAction.controls[0].displayName);
                }
            }
            return controlTips;
        }

        private void OnControlsChanged(PlayerInput playerInput) => _signalBus.Fire<ControlsChangedSignal>();

        #region Input Actions Callbacks
        private void OnShowPlayerMenuPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new ShowPlayerMenuPerformedSignal());

        private void OnHidePlayerMenuPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new HidePlayerMenuPerformedSignal());

        private void OnPlayer_PickItemPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new Player_PickItemPerformedSignal());

        private void OnPlayer_InteractPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new Player_InteractPerformedSignal());

        private void OnPlayer_SelectQuickAccessCellPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new Player_SelectQuickAccessCellPerformedSignal(context));

        private void OnPlayer_SelectNeighboringQuickAccessCellPerformedSignal(InputAction.CallbackContext context) => _signalBus.Fire(new Player_SelectNeighboringQuickAccessCellPerformedSignal(context));

        private void OnPlayer_ShowQuestProgressPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new Player_ShowQuestProgressPerformedSignal());

        private void OnItemsContainerMenu_CloseContainerPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new ItemsContainerMenu_CloseContainerPerformedSignal());

        private void OnItemsContainerMenu_NavigatePerformed(InputAction.CallbackContext context) => _signalBus.Fire(new ItemsContainerMenu_NavigatePerformedSignal(context));

        private void OnItemsContainerMenu_PickItemPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new ItemsContainerMenu_PickItemPerformedSignal());

        private void OnItemsContainerMenu_PickAllItemsPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new ItemsContainerMenu_PickAllItemsPerformedSignal());

        private void OnPlayerMenuCraftSection_CreateItemPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuCraftSection_CreateItemPerformedSignal());

        private void OnPlayerMenuInventorySection_NavigatePlayerMenuPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuInventorySection_NavigatePlayerMenuPerformedSignal(context));

        private void OnPlayerMenuInventorySection_NavigateSectionPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuInventorySection_NavigateSectionPerformedSignal(context));

        private void OnPlayerMenuItemsCountSelectPanel_CancelPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuItemsCountSelectPanel_CancelPerformedSignal());

        private void OnPlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformedSignal(context));

        private void OnPlayerMenuItemsCountSelectPanel_SelectPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuItemsCountSelectPanel_SelectPerformedSignal());

        private void OnPlayerMenuItemsCountSelectPanel_SelectAllPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuItemsCountSelectPanel_SelectAllPerformedSignal());

        private void OnPlayerMenuItemsInteractionsPanel_NavigatePerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuItemsInteractionsPanel_NavigatePerformedSignal(context));

        private void OnPlayerMenuItemsInteractionsPanel_SelectPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuItemsInteractionsPanel_SelectPerformedSignal());

        private void OnPlayerMenuItemsInteractionsPanel_CloseItemCellActionsMenuPerformed(InputAction.CallbackContext context) => _signalBus.Fire(new PlayerMenuItemsInteractionsPanel_CloseItemCellActionsMenuPerformedSignal());
        #endregion
    }
}