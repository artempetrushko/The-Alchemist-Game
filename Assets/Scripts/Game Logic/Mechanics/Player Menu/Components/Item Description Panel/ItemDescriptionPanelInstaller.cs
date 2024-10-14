using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemDescriptionPanelInstaller : MonoInstaller
    {
        [SerializeField] private ItemDescriptionPanelView _view;

        public override void InstallBindings()
        {
            Container.Bind<ItemDescriptionPanelPresenter>().AsSingle().NonLazy();
            Container.Bind<ItemDescriptionPanelView>().FromInstance(_view).AsSingle().NonLazy();
        }
    }
}