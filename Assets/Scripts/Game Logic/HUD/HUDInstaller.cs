using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class HUDInstaller : MonoInstaller
    {
        [SerializeField] private HUDView _view;

        public override void InstallBindings()
        {
            Container.Bind<HUDPresenter>().AsSingle().NonLazy();
            Container.Bind<HUDView>().FromInstance(_view).AsSingle().NonLazy();
        }
    }
}