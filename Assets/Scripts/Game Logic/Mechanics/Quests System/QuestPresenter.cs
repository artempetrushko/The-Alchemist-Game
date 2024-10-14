using System;
using Controls;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

namespace GameLogic.QuestSystem
{
    public class QuestPresenter : IDisposable
    {
        private const int QUEST_SHOWING_TIME_IN_SECONDS = 5;

        private QuestProgressView _questProgressView;
        private InputManager _inputManager;

        private Quest _currentQuest;
        private bool _isQuestShown;

        public QuestPresenter()
        {
            _inputManager.PlayerActions.Player.ShowQuestProgress.performed += OnShowQuestProgressActionPerformed;
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.Player.ShowQuestProgress.performed -= OnShowQuestProgressActionPerformed;
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

        private void OnShowQuestProgressActionPerformed(InputAction.CallbackContext context) => ShowQuestProgressAsync().Forget();
    }
}