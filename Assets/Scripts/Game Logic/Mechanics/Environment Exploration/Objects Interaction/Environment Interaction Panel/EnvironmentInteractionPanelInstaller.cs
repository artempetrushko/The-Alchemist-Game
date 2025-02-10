using UnityEngine;
using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class EnvironmentInteractionPanelInstaller : MonoInstaller
    {
        [SerializeField] private EnvironmentInteractionPanelView _view;

        public override void InstallBindings()
        {
            Container.Bind<EnvironmentInteractionPanelPresenter>().AsSingle().NonLazy();
            Container.Bind<EnvironmentInteractionPanelView>().FromInstance(_view).AsSingle().NonLazy();
        }
    }
}