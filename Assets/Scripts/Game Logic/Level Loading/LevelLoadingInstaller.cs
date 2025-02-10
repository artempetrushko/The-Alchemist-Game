using UnityEngine;
using Zenject;

namespace GameLogic.LevelLoading
{
    public class LevelLoadingInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoadingConfig _config;
        [SerializeField] private LevelLoadingView _view;

        public override void InstallBindings()
        {
            Container.Bind<LevelLoadingPresenter>().AsSingle().NonLazy();
            Container.Bind<LevelLoadingView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<LevelLoadingConfig>().FromScriptableObject(_config).AsSingle().NonLazy();

            Container.DeclareSignal<NextSceneSelectedSignal>();
        }
    }
}