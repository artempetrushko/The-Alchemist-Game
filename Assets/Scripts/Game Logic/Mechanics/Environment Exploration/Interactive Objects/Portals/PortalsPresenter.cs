using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameLogic.EnvironmentExploration
{
    public class PortalsPresenter : IDisposable
    {
        private const float VISIBILITY_CHANGING_DURATION = 1f;

        private PortalStabilizationProgressView _portalStabilizationProgressView;
        private SignalBus _signalBus;

        public PortalsPresenter(PortalStabilizationProgressView portalStabilizationProgressView, SignalBus signalBus)
        {
            _portalStabilizationProgressView = portalStabilizationProgressView;
            _signalBus = signalBus;

            _signalBus.Subscribe<ObjectInteractionStartedSignal>(OnObjectInteractionStarted);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ObjectInteractionStartedSignal>(OnObjectInteractionStarted);
        }

        private async UniTask StabilizePortalAsync(DungeonPortal portal)
        {
            portal.SetInteractionAvailability(false);

            var stabilizationEffect = Object.Instantiate(portal.Config.StabilizationEffectPrefab, portal.transform);

            EnablePortalEnemySpawnerAfterDelayAsync(portal).Forget();

            //await ActivateEnemiesSpawnerAsync();

            Object.Destroy(stabilizationEffect);
            portal.SetEnemySpawnerActive(false);

            portal.IsStable = true;
            portal.SetInteractionAvailability(true);
        }

        private async UniTask EnablePortalEnemySpawnerAfterDelayAsync(DungeonPortal portal)
        {
            await UniTask.WaitForSeconds(portal.Config.SpawnerAppearanceDelayInSeconds);
            portal.SetEnemySpawnerActive(true);
        }

        private async UniTask SetSectionVisibilityAsync(bool isVisible)
        {
            await _portalStabilizationProgressView.CanvasGroup
                .DOFade(isVisible ? 1f : 0f, VISIBILITY_CHANGING_DURATION)
                .AsyncWaitForCompletion();
            _portalStabilizationProgressView.SetProgressBarFillingAreaActive(isVisible);
        }

        private void SetProgressBarFillingPercent(float percent)
        {
            var progressBarSizeDelta = new Vector2(_portalStabilizationProgressView.ProgressBarRect.width * (0.1f + percent / 100), _portalStabilizationProgressView.ProgressBarFilingAreaRect.height);
            _portalStabilizationProgressView.SetProgressBarFillingAreaSizeDelta(progressBarSizeDelta);
        }

        private void OnObjectInteractionStarted(ObjectInteractionStartedSignal signal)
        {
            if (signal.InteractiveObject is DungeonPortal portal)
            {

            }
        }     
    }
}