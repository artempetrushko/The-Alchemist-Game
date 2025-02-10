using GameLogic.Player;
using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class EnvironmentInteractionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<InteractiveObjectDetectedSignal>();
            Container.DeclareSignal<InteractiveObjectLostSignal>();
        }
    }
}