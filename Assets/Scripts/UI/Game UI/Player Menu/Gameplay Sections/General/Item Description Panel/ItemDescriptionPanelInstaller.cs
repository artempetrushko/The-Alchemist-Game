using UnityEngine;
using Zenject;

namespace UI.PlayerMenu
{
    public class ItemDescriptionPanelInstaller : MonoInstaller
    {
        [SerializeField] private ItemDescriptionPanelView _itemDescriptionPanelView;
        [SerializeField] private ItemParameterView _itemParameterView;

        public override void InstallBindings()
        {
            Container.Bind<ItemDescriptionPanelController>().AsSingle().NonLazy();
            Container.Bind<ItemDescriptionPanelView>().FromInstance(_itemDescriptionPanelView).AsSingle().NonLazy();
            Container.Bind<ItemParameterView>().FromInstance(_itemParameterView).AsSingle().NonLazy();
        }
    }
}