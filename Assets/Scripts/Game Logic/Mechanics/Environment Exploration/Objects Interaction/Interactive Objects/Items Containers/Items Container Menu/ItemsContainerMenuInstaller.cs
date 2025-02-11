using Controls;
using UnityEngine;
using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsContainerMenuInstaller : MonoInstaller
    {
        [SerializeField] private ItemsContainerMenuView _view;
        [SerializeField] private ItemsContainerActionMap _actionMap;

        public override void InstallBindings()
        {
            Container.Bind<ItemsContainerMenuPresenter>().AsSingle().NonLazy();
            Container.Bind<ItemsContainerMenuView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<ItemsContainerActionMap>().FromScriptableObject(_actionMap).AsSingle().NonLazy();
        }
    }
}