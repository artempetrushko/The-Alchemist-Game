using UnityEngine;
using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class ItemPickingMessagesPanelInstaller : MonoInstaller
    {
        [SerializeField] private ItemPickingMessagesPanelConfig _config;
        [SerializeField] private ItemPickingMessagesPanelView _view;

        public override void InstallBindings()
        {
            Container.Bind<ItemPickingMessagesPanelPresenter>().AsSingle().NonLazy();
            Container.Bind<ItemPickingMessagesPanelView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<ItemPickingMessagesPanelConfig>().FromScriptableObject(_config).AsSingle().NonLazy();
        }
    }
}