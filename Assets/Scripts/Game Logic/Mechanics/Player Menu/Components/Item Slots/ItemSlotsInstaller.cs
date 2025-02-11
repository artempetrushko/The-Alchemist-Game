using UnityEngine;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemSlotsInstaller : MonoInstaller
    {
        [SerializeField] private MovingItemView _movingItemView;

        public override void InstallBindings()
        {
            Container.Bind<ItemSlotsController>().AsSingle().NonLazy();
            Container.Bind<MovingItemView>().FromInstance(_movingItemView).AsSingle().NonLazy();
        }
    }
}