using System;
using Cysharp.Threading.Tasks;
using EventBus;
using Zenject;

namespace GameLogic.QuestSystem
{
    public class QuestPresenter : IDisposable
    {
        private const int QUEST_SHOWING_TIME_IN_SECONDS = 5;

        private QuestProgressView _questProgressView;
        private SignalBus _signalBus;

        private Quest _currentQuest;
        private bool _isQuestShown;

        public QuestPresenter(QuestProgressView questProgressView, SignalBus signalBus)
        {
            _questProgressView = questProgressView;
            _signalBus = signalBus;

            _signalBus.Subscribe<Player_ShowQuestProgressPerformedSignal>(OnShowQuestProgressActionPerformed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<Player_ShowQuestProgressPerformedSignal>(OnShowQuestProgressActionPerformed);
        }

        public void StartFirstQuest(Quest quest, GameProgress gameProgress)
        {
            _currentQuest = quest;
            UpdateCurrentQuestInfo(gameProgress);
        }

        public void UpdateCurrentQuestInfo(GameProgress gameProgress)
        {
            if (_currentQuest.IsCompleted(gameProgress))
            {
                _currentQuest = _currentQuest.NextQuest;
            }

            var currentQuestDescription = _currentQuest.Description.GetLocalizedString();
            if (_currentQuest is IProgressiveQuest progressiveQuest)
            {
                var formattedQuestProgress = progressiveQuest.GetFormattedProgress(gameProgress);
                currentQuestDescription += $"({formattedQuestProgress})";
            }
            _questProgressView.SetQuestDescriptionText(currentQuestDescription);

            ShowQuestProgressAsync().Forget();
        }

        private async UniTask ShowQuestProgressAsync()
        {
            if (!_isQuestShown)
            {
                _isQuestShown = true;

                _questProgressView.SetActive(true);
                await _questProgressView.PlayFadeAnimation(1f, 1.5f);
                await UniTask.WaitForSeconds(QUEST_SHOWING_TIME_IN_SECONDS);
                await _questProgressView.PlayFadeAnimation(0f, 1.5f);
                _questProgressView.SetActive(false);

                _isQuestShown = false;
            }
        }

        private void OnShowQuestProgressActionPerformed(Player_ShowQuestProgressPerformedSignal signal) => ShowQuestProgressAsync().Forget();
    }
}