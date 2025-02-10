using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class HealthCounterInstaller : MonoInstaller
    {
        [SerializeField] private HealthCounterConfig _config;
        [SerializeField] private HealthCounterView _view;

        public override void InstallBindings()
        {
            Container.Bind<HealthCounterPresenter>().AsSingle().NonLazy();
            Container.Bind<HealthCounterView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<HealthCounterConfig>().FromScriptableObject(_config).AsSingle().NonLazy();
        }
    }
}