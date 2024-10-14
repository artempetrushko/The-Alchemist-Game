using UnityEngine;
using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class ItemPickingInstaller : MonoInstaller
	{
        [SerializeField] private ItemPickingMessagesSectionConfig _config;
        [SerializeField] private ItemPickingMessagesSectionView _view;

        public override void InstallBindings()
        {
            Container.Bind<ItemPickingMessagesSectionPresenter>().AsSingle().NonLazy();
            Container.Bind<ItemPickingMessagesSectionView>().FromInstance(_view).AsSingle().NonLazy();
            Container.Bind<ItemPickingMessagesSectionConfig>().FromScriptableObject(_config).AsSingle().NonLazy();

            Container.DeclareSignal<ItemPickingRequestedSignal>();
            Container.DeclareSignalWithInterfaces<ItemPickedSignal>();
        }
    }
}