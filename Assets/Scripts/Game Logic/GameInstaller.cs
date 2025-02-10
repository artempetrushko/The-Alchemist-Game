using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _config;

        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.Bind<GameConfig>().FromScriptableObject(_config).AsSingle().NonLazy();

            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<RunFinishedSignal>();
        }
    }
}