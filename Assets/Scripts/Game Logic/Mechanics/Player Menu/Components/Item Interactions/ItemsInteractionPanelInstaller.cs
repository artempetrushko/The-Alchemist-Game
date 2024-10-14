using Controls;
using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemsInteractionPanelInstaller : MonoInstaller
    {
        [SerializeField] private ItemsInteractionsPanelView _view;
        [SerializeField] private PlayerMenuItemSlotActionsMenuActionMap _actionMap;

        public override void InstallBindings()
        {
            Container.Bind<ItemsInteractionsPanelPresenter>().AsSingle().NonLazy();
            Container.Bind<ItemsInteractionsPanelView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<PlayerMenuItemSlotActionsMenuActionMap>().FromScriptableObject(_actionMap).AsSingle().NonLazy();
        }
    }
}