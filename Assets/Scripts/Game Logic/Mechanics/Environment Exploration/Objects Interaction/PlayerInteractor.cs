using Controls;
using EventBus;
using GameLogic.EnvironmentExploration;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GameLogic.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        private InputManager _inputManager;
        private SignalBus _signalBus;
        private InteractiveObject _currentInteractiveObject;

        [Inject]
        public void Construct(InputManager inputManager, SignalBus signalBus)
        {
            _inputManager = inputManager;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _inputManager.PlayerActions.Player.Interact.performed += OnInteractActionPerformed;
        }

        private void OnDisable()
        {
            _inputManager.PlayerActions.Player.Interact.performed -= OnInteractActionPerformed;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<InteractiveObject>(out var potentialInteractiveObject))
            {
                _currentInteractiveObject = potentialInteractiveObject;
                _signalBus.Fire(new InteractiveObjectDetectedSignal(potentialInteractiveObject));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<InteractiveObject>() != null)
            {
                _signalBus.Fire<InteractiveObjectLostSignal>();
            }
        }

        private void OnInteractActionPerformed(InputAction.CallbackContext context) => _currentInteractiveObject.Interact();
    }
}