using Cysharp.Threading.Tasks;
using EventBus;
using GameLogic.QuestSystem;
using Zenject;

namespace GameLogic
{
    public class GameManager
    {
        private GameConfig _config;
        private GameProgress _gameProgress;
        private QuestPresenter _questPresenter;
        private HUDPresenter _hudPresenter;
        private SignalBus _signalBus;

        public GameManager(GameConfig config, QuestPresenter questPresenter, HUDPresenter hudPresenter, SignalBus signalBus)
        {
            _config = config;
            _gameProgress = new GameProgress();
            _questPresenter = questPresenter;
            _hudPresenter = hudPresenter;
            _signalBus = signalBus;
        }

        public async UniTask StartGameAsync()
        {
            await _hudPresenter.HideStartBlackScreenAsync();

            await _hudPresenter.ShowLocationNameAsync(_config.LocationName.GetLocalizedString());

            _questPresenter.StartFirstQuest(_config.StartQuest, _gameProgress);
        }

        public void FinishRun()
        {
            _signalBus.Fire(new RunFinishedSignal(RunEndingStatus.Completion));
        }
    }
}