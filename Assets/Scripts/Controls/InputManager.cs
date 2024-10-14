using System;
using System.Linq;
using UnityEngine.InputSystem;
using Zenject;

namespace Controls
{
    public class InputManager
    {
        public readonly PlayerInputActions PlayerActions;

        private InputConfig _inputConfig;
        private PlayerInput _playerInput;
        private SignalBus _signalBus;
        private PlayerInputActionMap _currentActionMap;
        private PlayerInputActionMap _previousActionMap;

        public InputManager(PlayerInput playerInput, InputConfig inputConfig, SignalBus signalBus)
        {
            _playerInput = playerInput;
            _inputConfig = inputConfig;
            _signalBus = signalBus;

            PlayerActions = new PlayerInputActions();
            _currentActionMap = _inputConfig.DefaultActionMap;

            _playerInput.controlsChangedEvent.AddListener(OnControlsChanged);
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
    }
}