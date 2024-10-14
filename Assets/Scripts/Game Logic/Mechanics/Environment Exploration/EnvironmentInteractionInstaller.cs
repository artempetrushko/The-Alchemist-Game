using GameLogic.Player;
using UnityEngine;
using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class EnvironmentInteractionInstaller : MonoInstaller
    {
        [SerializeField] private EnvironmentInteractionView _view;

        public override void InstallBindings()
        {
            Container.Bind<EnvironmentInteractionPresenter>().AsSingle().NonLazy();
            Container.Bind<EnvironmentInteractionView>().FromInstance(_view).AsSingle().NonLazy();

            Container.DeclareSignal<InteractiveObjectDetectedSignal>();
            Container.DeclareSignal<InteractiveObjectLostSignal>();
            Container.DeclareSignal<ObjectInteractionStartedSignal>();
        }
    }
}