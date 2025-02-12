using Controls;
using EventBus;
using GameLogic.EnvironmentExploration;
using UnityEngine;
using Zenject;

namespace GameLogic.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        private SignalBus _signalBus;
        private InteractiveObject _currentInteractiveObject;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<Player_InteractPerformedSignal>(OnInteractActionPerformed);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<Player_InteractPerformedSignal>(OnInteractActionPerformed);
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

        private void OnInteractActionPerformed(Player_InteractPerformedSignal signal) => _currentInteractiveObject.Interact();
    }
}