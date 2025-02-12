using System;
using System.Linq;
using Controls;
using Cysharp.Threading.Tasks;
using EventBus;
using Zenject;

namespace GameLogic
{
    public class RunEndingPanelPresenter : IDisposable
    {
        private RunEndingPanelConfig _config;
        private RunEndingPanelView _view;
        private SignalBus _signalBus;

        public RunEndingPanelPresenter(RunEndingPanelConfig config, RunEndingPanelView view, SignalBus signalBus)
        {
            _config = config;
            _view = view;
            _signalBus = signalBus;

            _view.ReturnToHubButton.ButtonComponent.onClick.AddListener(OnReturnToHubButtonPressed);
            _view.ExitToMainMenuButton.ButtonComponent.onClick.AddListener(OnExitToMainMenuButtonPressed);

            _signalBus.Subscribe<RunFinishedSignal>(OnRunFinished);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RunFinishedSignal>(OnRunFinished);
        }

        private void OnRunFinished(RunFinishedSignal signal)
        {
            _signalBus.Fire(new ActionMapRequestedSignal(_config.ActionMap));

            _view.SetActive(true);

            var runEndingStatusData = _config.StatusDatas.FirstOrDefault(statusData => statusData.Status == signal.RunEndingStatus);
            _view.RunEndingMessageView.SetMessageIcon(runEndingStatusData.StatusIcon);
            _view.RunEndingMessageView.SetMessageText(runEndingStatusData.StatusDescription.GetLocalizedString());

            //var controlTips = _inputManager.GetCurrentControlTips();
            //TODO: реализовать подсказки по управлению

            _view.ShowAsync().Forget();
        }

        private void OnReturnToHubButtonPressed() { }

        private void OnExitToMainMenuButtonPressed() { }
    }
}