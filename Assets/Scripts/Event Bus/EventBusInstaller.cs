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

            Container.DeclareSignal<ItemDroppedSignal>();

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

            DeclareControlsSignals();
        }

        private void DeclareControlsSignals()
        {
            Container.DeclareSignal<ActionMapRequestedSignal>();
            Container.DeclareSignal<PreviousActionMapRequestedSignal>();

            Container.DeclareSignal<ShowPlayerMenuPerformedSignal>();
            Container.DeclareSignal<HidePlayerMenuPerformedSignal>();

            Container.DeclareSignal<ItemsContainerMenu_CloseContainerPerformedSignal>();
            Container.DeclareSignal<ItemsContainerMenu_NavigatePerformedSignal>();
            Container.DeclareSignal<ItemsContainerMenu_PickAllItemsPerformedSignal>();
            Container.DeclareSignal<ItemsContainerMenu_PickItemPerformedSignal>();

            Container.DeclareSignal<Player_InteractPerformedSignal>();
            Container.DeclareSignal<Player_PickItemPerformedSignal>();
            Container.DeclareSignal<Player_SelectNeighboringQuickAccessCellPerformedSignal>();
            Container.DeclareSignal<Player_SelectQuickAccessCellPerformedSignal>();
            Container.DeclareSignal<Player_ShowQuestProgressPerformedSignal>();

            Container.DeclareSignal<PlayerMenuCraftSection_CreateItemPerformedSignal>();

            Container.DeclareSignal<PlayerMenuInventorySection_NavigatePlayerMenuPerformedSignal>();
            Container.DeclareSignal<PlayerMenuInventorySection_NavigateSectionPerformedSignal>();

            Container.DeclareSignal<PlayerMenuItemsCountSelectPanel_CancelPerformedSignal>();
            Container.DeclareSignal<PlayerMenuItemsCountSelectPanel_ChangeItemsCountPerformedSignal>();
            Container.DeclareSignal<PlayerMenuItemsCountSelectPanel_SelectAllPerformedSignal>();
            Container.DeclareSignal<PlayerMenuItemsCountSelectPanel_SelectPerformedSignal>();

            Container.DeclareSignal<PlayerMenuItemsInteractionsPanel_CloseItemCellActionsMenuPerformedSignal>();
            Container.DeclareSignal<PlayerMenuItemsInteractionsPanel_NavigatePerformedSignal>();
            Container.DeclareSignal<PlayerMenuItemsInteractionsPanel_SelectPerformedSignal>();
        }
    }
}