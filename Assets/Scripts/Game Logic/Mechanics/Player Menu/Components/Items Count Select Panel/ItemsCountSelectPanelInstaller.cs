using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemsCountSelectPanelInstaller : MonoInstaller
    {
        [SerializeField] private ItemsCountSelectPanelConfig _config;
        [SerializeField] private ItemsCountSelectPanelView _view;

        public override void InstallBindings()
        {
            Container.Bind<ItemsCountSelectPanelPresenter>().AsSingle().NonLazy();
            Container.Bind<ItemsCountSelectPanelView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<ItemsCountSelectPanelConfig>().FromScriptableObject(_config).AsSingle().NonLazy();
        }
    }
}