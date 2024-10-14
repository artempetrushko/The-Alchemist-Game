using System;
using System.Linq;
using Controls;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameLogic.LevelLoading
{
    public class LevelLoadingPresenter : IDisposable
    {
        private LevelLoadingConfig _config;
        private LevelLoadingView _view;
        private InputManager _inputManager;
        private SignalBus _signalBus;
        private AsyncOperation _loadingOperation;
        private bool _isLevelLoadingFinished = false;

        public LevelLoadingPresenter(LevelLoadingConfig config, LevelLoadingView view, InputManager inputManager, SignalBus signalBus)
        {
            _config = config;
            _view = view;
            _inputManager = inputManager;
            _signalBus = signalBus;

            //TODO: доделать взаимодействие с системой ввода
            // _inputManager.PlayerActions

            _signalBus.Subscribe<NextSceneSelectedSignal>(OnNextSceneSelected);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<NextSceneSelectedSignal>(OnNextSceneSelected);
        }

        public string GetCurrentLevelName()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            var currentSceneConfig = _config.SceneConfigs.First(sceneConfig => sceneConfig.SceneIndex == currentSceneIndex);
            return currentSceneConfig.Name.GetLocalizedString();
        }

        private void ContinueSceneLoadingByInput(InputAction.CallbackContext context)
        {
            if (_loadingOperation != null && _isLevelLoadingFinished)
            {
                _loadingOperation.allowSceneActivation = true;
            }
        }

        private async UniTask LoadLevelAsync(int loadingSceneIndex)
        {
            _inputManager.SetActionMap(_config.ActionMap);
            _view.SetActive(true);          

            _loadingOperation = SceneManager.LoadSceneAsync(loadingSceneIndex);
            _loadingOperation.allowSceneActivation = false;
            while (!_loadingOperation.isDone)
            {
                if (_loadingOperation.progress >= 0.9f)
                {
                    _isLevelLoadingFinished = true;

                    _view.SetContinueTipSectionActive(true);
                    _view.SetLoadingInfoSectionAlpha(0f);
                }
                else
                {
                    _view.SetProgressBarFillAmount(_loadingOperation.progress);
                }
                await UniTask.Yield();
            }
        }
        
        private void OnNextSceneSelected(NextSceneSelectedSignal signal)
        {
            var nextSceneConfig = _config.SceneConfigs.FirstOrDefault(levelConfig => levelConfig.SceneType == signal.SceneType);
            if (nextSceneConfig != null)
            {
                LoadLevelAsync(nextSceneConfig.SceneIndex).Forget();
            }
        }
    }
}