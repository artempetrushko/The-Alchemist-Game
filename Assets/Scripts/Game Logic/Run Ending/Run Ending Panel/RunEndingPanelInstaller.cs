using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class RunEndingPanelInstaller : MonoInstaller
    {
        [SerializeField] private RunEndingPanelConfig _config;
        [SerializeField] private RunEndingPanelView _view;

        public override void InstallBindings()
        {
            Container.Bind<RunEndingPanelPresenter>().AsSingle().NonLazy();
            Container.Bind<RunEndingPanelView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<RunEndingPanelConfig>().FromScriptableObject(_config).AsSingle().NonLazy();
        }
    }
}