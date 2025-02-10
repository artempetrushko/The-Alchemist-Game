using Zenject;

namespace GameLogic.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<PlayerHealthChangedSignal>();
        }
    }
}