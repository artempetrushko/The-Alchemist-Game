using UnityEngine;
using Zenject;

namespace GameLogic.HUD
{
    public class HUDInstaller : MonoInstaller
    {
        [SerializeField] private HUDView _hudView;

        public override void InstallBindings()
        {
            Container.Bind<HUDManager>().AsSingle().NonLazy();
            Container.Bind<HUDView>().FromInstance(_hudView).AsSingle().NonLazy();
        }
    }
}