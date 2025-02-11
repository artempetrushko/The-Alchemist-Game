using Zenject;

namespace EventBus
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ControlsChangedSignal>();

            Container.DeclareSignal<WeaponChangedSignal>();

            Container.DeclareSignal<RunFinishedSignal>();

            Container.DeclareSignal<PlayerHealthChangedSignal>();

            Container.DeclareSignal<NextSceneSelectedSignal>();

            Container.DeclareSignal<ItemDroppedSignal>();
            Container.DeclareSignal<MinimizedInventoryEnabledSignal>();
            Container.DeclareSignal<MinimizedInventoryDisabledSignal>();

            Container.DeclareSignal<ActionButtonControlTipColorChangedSignal>();

            Container.DeclareSignal<PlayerMenuEnabledSignal>();
            Container.DeclareSignal<PlayerMenuDisabledSignal>();
            Container.DeclareSignal<ItemMovingStartedSignal>();
            Container.DeclareSignal<ItemMovingFinishedSignal>();
            Container.DeclareSignal<InteractableElementSelectedByPointerSignal>();
            Container.DeclareSignal<SelectedItemSlotChangedSignal>();

            Container.DeclareSignal<InteractiveObjectDetectedSignal>();
            Container.DeclareSignal<InteractiveObjectLostSignal>();

            Container.DeclareSignal<FilledItemSlotPointerEnterSignal>();
            Container.DeclareSignal<FilledItemSlotPointerExitSignal>();
            Container.DeclareSignal<FilledItemSlotPointerDownSignal>();
            Container.DeclareSignal<ItemDraggingRequestedSignal>();
            Container.DeclareSignal<ItemDraggingFinishedSignal>();

            Container.DeclareSignalWithInterfaces<ContainedItemPickedSignal>();
            Container.DeclareSignalWithInterfaces<ItemPickedSignal>();
            Container.DeclareSignal<ItemPickingRequestedSignal>();           
            Container.DeclareSignal<PickableItemPickingRequestedSignal>();
            Container.DeclareSignal<PickableItemPickedSignal>();

            Container.DeclareSignal<EnergySlotItemChangedSignal>();
            Container.DeclareSignal<IngredientSlotItemChangedSignal>();
            Container.DeclareSignal<ItemCraftedSignal>();
        }
    }
}