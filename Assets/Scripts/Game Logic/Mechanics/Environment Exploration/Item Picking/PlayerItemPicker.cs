using System.Collections.Generic;
using Controls;
using EventBus;
using GameLogic.EnvironmentExploration;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GameLogic.Player
{
    public class PlayerItemPicker : MonoBehaviour
    {
        private InputManager _inputManager;
        private SignalBus _signalBus;

        private List<PickableItem> _availablePickableItems = new();

        [Inject]
        public void Construct(InputManager inputManager, SignalBus signalBus)
        {
            _inputManager = inputManager;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<PickableItemPickedSignal>(OnPickableItemPicked);
            _inputManager.PlayerActions.Player.PickItem.performed += OnPickItemActionPerformed;
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<PickableItemPickedSignal>(OnPickableItemPicked);
            _inputManager.PlayerActions.Player.PickItem.performed -= OnPickItemActionPerformed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PickableItem>(out var pickableItem))
            {
                _availablePickableItems.Add(pickableItem);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PickableItem>(out var pickableItem))
            {
                _availablePickableItems.Remove(pickableItem);
            }
        }

        private void OnPickItemActionPerformed(InputAction.CallbackContext context)
        {
            if (_availablePickableItems.Count > 0)
            {
                foreach (var pickableItem in _availablePickableItems)
                {
                    _signalBus.Fire(new PickableItemPickingRequestedSignal(pickableItem));
                }
                _availablePickableItems.Clear();
            }
        }

        private void OnPickableItemPicked(PickableItemPickedSignal signal) => Destroy(signal.PickableItem.gameObject);
    }
}