using System;
using Controls;
using EventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemsInteractionsPanelPresenter : IDisposable
    {
        private ItemsInteractionsPanelModel _model;
        private ItemsInteractionsPanelView _view;
        private ItemsInteractionsPanelActionMap _actionMap;
        private SignalBus _signalBus;

        public ItemsInteractionsPanelPresenter(ItemsInteractionsPanelView view, ItemsInteractionsPanelActionMap actionMap, SignalBus signalBus)
        {
            _view = view;
            _actionMap = actionMap;
            _signalBus = signalBus;

            _signalBus.Subscribe<FilledItemSlotPointerDownSignal>(OnFilledItemSlotPointerDown);

            _signalBus.Subscribe<PlayerMenuItemsInteractionsPanel_CloseItemCellActionsMenuPerformedSignal>(OnCloseItemsInteractionsPanelActionPerformed);
            _signalBus.Subscribe<PlayerMenuItemsInteractionsPanel_NavigatePerformedSignal>(OnNavigateInteractionsActionPerformed);
            _signalBus.Subscribe<PlayerMenuItemsInteractionsPanel_SelectPerformedSignal>(OnSelectInteractionActionPerformed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FilledItemSlotPointerDownSignal>(OnFilledItemSlotPointerDown);

            _signalBus.Unsubscribe<PlayerMenuItemsInteractionsPanel_CloseItemCellActionsMenuPerformedSignal>(OnCloseItemsInteractionsPanelActionPerformed);
            _signalBus.Unsubscribe<PlayerMenuItemsInteractionsPanel_NavigatePerformedSignal>(OnNavigateInteractionsActionPerformed);
            _signalBus.Unsubscribe<PlayerMenuItemsInteractionsPanel_SelectPerformedSignal>(OnSelectInteractionActionPerformed);
        }

        private void ClearActionsMenu()
        {
            
        }

        private void OnFilledItemSlotPointerDown(FilledItemSlotPointerDownSignal signal)
        {
            _signalBus.Fire(new ActionMapRequestedSignal(_actionMap));

            _view.SetActive(true);

            var interactions = signal.ItemSlot.InteractionsConfig.Interactions;
            for (var i = 0; i < interactions.Length; i++)
            {
                var interactionButton = _view.GetOrCreateInteractionButtonByIndex(i);
                interactionButton.SetActive(true);
                interactionButton.SetInteractionIcon(interactions[i].Icon);
                interactionButton.SetInteractionTitleText(interactions[i].DisplayedName.GetLocalizedString());
                interactionButton.ButtonComponent.onClick.AddListener(() => OnInteractionButtonPressed(interactionButton));//TODO
            }
        }

        private void OnInteractionButtonPressed(ItemsInteractionButton interactionButton)
        {

        }

        private void OnNavigateInteractionsActionPerformed(PlayerMenuItemsInteractionsPanel_NavigatePerformedSignal signal)
        {
            var inputValue = signal.Context.ReadValue<Vector2>();
            if (Mathf.Abs(inputValue.y) == 1)
            {
                _model.SelectedInteractionNumber -= (int)inputValue.y;
            }
        }

        private void OnSelectInteractionActionPerformed(PlayerMenuItemsInteractionsPanel_SelectPerformedSignal signal) => _model.InteractionInfos[_model.SelectedInteractionNumber - 1].interaction.Activate(_model.SelectedItemSlot);

        private void OnCloseItemsInteractionsPanelActionPerformed(PlayerMenuItemsInteractionsPanel_CloseItemCellActionsMenuPerformedSignal signal)
        {
            ClearActionsMenu();
            _signalBus.Fire(new PreviousActionMapRequestedSignal());
        }
    }
}