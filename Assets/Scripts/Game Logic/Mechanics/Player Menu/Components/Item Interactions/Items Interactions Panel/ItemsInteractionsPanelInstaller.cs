using Controls;
using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemsInteractionsPanelInstaller : MonoInstaller
    {
        [SerializeField] private ItemsInteractionsPanelView _view;
        [SerializeField] private ItemsInteractionsPanelActionMap _actionMap;

        public override void InstallBindings()
        {
            Container.Bind<ItemsInteractionsPanelPresenter>().AsSingle().NonLazy();
            Container.Bind<ItemsInteractionsPanelView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<ItemsInteractionsPanelActionMap>().FromScriptableObject(_actionMap).AsSingle().NonLazy();
        }
    }
}