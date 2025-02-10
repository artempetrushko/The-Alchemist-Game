using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class PlayerMenuInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMenuConfig _config;
        [SerializeField] private PlayerMenuView _view;

        public override void InstallBindings()
        {
            Container.Bind<PlayerMenuPresenter>().AsSingle().NonLazy();
            Container.Bind<PlayerMenuView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<PlayerMenuConfig>().FromScriptableObject(_config).AsSingle().NonLazy();

            Container.DeclareSignal<PlayerMenuEnabledSignal>();
            Container.DeclareSignal<PlayerMenuDisabledSignal>();
            Container.DeclareSignal<ItemMovingStartedSignal>();
            Container.DeclareSignal<ItemMovingFinishedSignal>();
            Container.DeclareSignal<InteractableElementSelectedByPointerSignal>();
            Container.DeclareSignal<SelectedItemSlotChangedSignal>();
        }
    }
}