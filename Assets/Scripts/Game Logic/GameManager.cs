using Cysharp.Threading.Tasks;
using EventBus;
using GameLogic.LevelLoading;
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
        private LevelLoadingPresenter _levelLoadingPresenter;
        private SignalBus _signalBus;

        public GameManager(GameConfig config, QuestPresenter questPresenter, HUDPresenter hudPresenter, LevelLoadingPresenter levelLoadingPresenter, SignalBus signalBus)
        {
            _config = config;
            _gameProgress = new GameProgress();
            _questPresenter = questPresenter;
            _levelLoadingPresenter = levelLoadingPresenter;
            _hudPresenter = hudPresenter;
            _signalBus = signalBus;
        }

        public async UniTask StartGameAsync()
        {
            await _hudPresenter.HideStartBlackScreenAsync();

            var locationName = _levelLoadingPresenter.GetCurrentLevelName();
            await _hudPresenter.ShowLocationNameAsync(locationName);

            _questPresenter.StartFirstQuest(_config.StartQuest, _gameProgress);
        }

        public void FinishRun()
        {
            _signalBus.Fire(new RunFinishedSignal(RunEndingStatus.Completion));
        }
    }
}