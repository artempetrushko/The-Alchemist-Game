using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class RunEndingInstaller : MonoInstaller
    {
        [SerializeField] private RunEndingConfig _config;
        [SerializeField] private RunEndingView _view;

        public override void InstallBindings()
        {
            Container.Bind<RunEndingPresenter>().AsSingle().NonLazy();
            Container.Bind<RunEndingView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<RunEndingConfig>().FromScriptableObject(_config).AsSingle().NonLazy();
        }
    }
}