using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class ItemPickingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<ItemPickingRequestedSignal>();
            Container.DeclareSignalWithInterfaces<ItemPickedSignal>();
            Container.DeclareSignal<PickableItemPickingRequestedSignal>();
            Container.DeclareSignal<PickableItemPickedSignal>();
        }
    }
}