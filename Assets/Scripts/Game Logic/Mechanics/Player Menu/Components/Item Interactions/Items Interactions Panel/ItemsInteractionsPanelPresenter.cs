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
        private InputManager _inputManager;
        private SignalBus _signalBus;

        public ItemsInteractionsPanelPresenter(ItemsInteractionsPanelView view, ItemsInteractionsPanelActionMap actionMap, InputManager inputManager, SignalBus signalBus)
        {
            _view = view;
            _actionMap = actionMap;
            _inputManager = inputManager;
            _signalBus = signalBus;

            _inputManager.PlayerActions.PlayerMenuItemsInteractionsPanel.Navigate.performed += OnNavigateInteractionsActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsInteractionsPanel.Select.performed += OnSelectInteractionActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsInteractionsPanel.CloseItemCellActionsMenu.performed += OnCloseItemsInteractionsPanelActionPerformed;

            _signalBus.Subscribe<FilledItemSlotPointerDownSignal>(OnFilledItemSlotPointerDown);
        }

        public void Dispose()
        {
            _inputManager.PlayerActions.PlayerMenuItemsInteractionsPanel.Navigate.performed -= OnNavigateInteractionsActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsInteractionsPanel.Select.performed -= OnSelectInteractionActionPerformed;
            _inputManager.PlayerActions.PlayerMenuItemsInteractionsPanel.CloseItemCellActionsMenu.performed -= OnCloseItemsInteractionsPanelActionPerformed;

            _signalBus.Unsubscribe<FilledItemSlotPointerDownSignal>(OnFilledItemSlotPointerDown);
        }

        private void ClearActionsMenu()
        {
            
        }

        private void OnFilledItemSlotPointerDown(FilledItemSlotPointerDownSignal signal)
        {
            _inputManager.SetActionMap(_actionMap);

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

        private void OnNavigateInteractionsActionPerformed(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<Vector2>();
            if (Mathf.Abs(inputValue.y) == 1)
            {
                _model.SelectedInteractionNumber -= (int)inputValue.y;
            }
        }

        private void OnSelectInteractionActionPerformed(InputAction.CallbackContext context) => _model.InteractionInfos[_model.SelectedInteractionNumber - 1].interaction.Activate(_model.SelectedItemSlot);

        private void OnCloseItemsInteractionsPanelActionPerformed(InputAction.CallbackContext context)
        {
            ClearActionsMenu();
            _inputManager.SetPreviousActionMap();
        }
    }
}