using System;
using System.Linq;
using Controls;
using Cysharp.Threading.Tasks;
using EventBus;
using UnityEngine.InputSystem;
using Zenject;

namespace GameLogic
{
    public class RunEndingPanelPresenter : IDisposable
    {
        private RunEndingPanelConfig _config;
        private RunEndingPanelView _view;
        private InputManager _inputManager;
        private SignalBus _signalBus;

        public RunEndingPanelPresenter(RunEndingPanelConfig config, RunEndingPanelView view, InputManager inputManager, SignalBus signalBus)
        {
            _config = config;
            _view = view;
            _inputManager = inputManager;
            _signalBus = signalBus;

            _view.ReturnToHubButton.ButtonComponent.onClick.AddListener(OnReturnToHubButtonPressed);
            _view.ExitToMainMenuButton.ButtonComponent.onClick.AddListener(OnExitToMainMenuButtonPressed);

            _inputManager.PlayerActions.RunEndingPanel.ReturnToHub.performed += OnReturnToHubActionPerformed;
            _inputManager.PlayerActions.RunEndingPanel.ExitToMainMenu.performed += OnExitToMainMenuActionPerformed;

            _signalBus.Subscribe<RunFinishedSignal>(OnRunFinished);
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.RunEndingPanel.ReturnToHub.performed -= OnReturnToHubActionPerformed;
            _inputManager.PlayerActions.RunEndingPanel.ExitToMainMenu.performed -= OnExitToMainMenuActionPerformed;

            _signalBus.Unsubscribe<RunFinishedSignal>(OnRunFinished);
        }

        private void OnRunFinished(RunFinishedSignal signal)
        {
            _inputManager.SetActionMap(_config.ActionMap);

            _view.SetActive(true);

            var runEndingStatusData = _config.StatusDatas.FirstOrDefault(statusData => statusData.Status == signal.RunEndingStatus);
            _view.RunEndingMessageView.SetMessageIcon(runEndingStatusData.StatusIcon);
            _view.RunEndingMessageView.SetMessageText(runEndingStatusData.StatusDescription.GetLocalizedString());

            var controlTips = _inputManager.GetCurrentControlTips();
            //TODO: реализовать подсказки по управлению

            _view.ShowAsync().Forget();
        }

        private void OnReturnToHubActionPerformed(InputAction.CallbackContext context) => OnReturnToHubButtonPressed();

        private void OnExitToMainMenuActionPerformed(InputAction.CallbackContext context) => OnExitToMainMenuButtonPressed();

        private void OnReturnToHubButtonPressed() { }

        private void OnExitToMainMenuButtonPressed() { }
    }
}