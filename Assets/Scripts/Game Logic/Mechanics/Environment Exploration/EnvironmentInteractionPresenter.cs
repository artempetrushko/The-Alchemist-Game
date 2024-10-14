using System;
using Controls;
using GameLogic.Player;
using UnityEngine;
using Zenject;

namespace GameLogic.EnvironmentExploration
{

    public class EnvironmentInteractionPresenter : IDisposable, ITickable
    {
        private const float VIEW_OFFSET_MULTIPLIER = 3f;

        private EnvironmentInteractionModel _model;
        private EnvironmentInteractionView _view;
        private InputManager _inputManager;
        private SignalBus _signalBus;

        public EnvironmentInteractionPresenter(EnvironmentInteractionView view, InputManager inputManager, SignalBus signalBus)
        {
            _view = view;
            _inputManager = inputManager;
            _signalBus = signalBus;

            _signalBus.Subscribe<InteractiveObjectDetectedSignal>(OnInteractiveObjectDetected);
            _signalBus.Subscribe<InteractiveObjectLostSignal>(OnInteractiveObjectLost);
            _signalBus.Subscribe<ControlsChangedSignal>(OnControlsChanged);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InteractiveObjectDetectedSignal>(OnInteractiveObjectDetected);
            _signalBus.Unsubscribe<InteractiveObjectLostSignal>(OnInteractiveObjectLost);
            _signalBus.Unsubscribe<ControlsChangedSignal>(OnControlsChanged);
        }

        public void Tick()
        {
            if (_view.isActiveAndEnabled)
            {
                var cameraRelativeViewPosition = (Vector2)Camera.main.WorldToScreenPoint(_model.ViewWorldPosition);
                _view.SetPosition(cameraRelativeViewPosition);
            }
        }

        private void ShowCurrentInteractiveObjectControlsTips()
        {
            var controlsTips = _inputManager.GetControlTips(new[]
            {
                (_model.InteractionDescription.GetLocalizedString(), _inputManager.PlayerActions.Player.Interact)
            });

            //TODO: реализовать подсказки по управлению
            //_view.ControlsTipsSectionView
        }

        private Vector3 GetViewWorldPosition(InteractiveObject interactiveObject)
        {
            var viewOffset = Vector3.zero;
            if (interactiveObject.TryGetComponent<MeshRenderer>(out var interactiveObjectMesh))
            {
                viewOffset.y = interactiveObjectMesh.bounds.size.y * VIEW_OFFSET_MULTIPLIER;
            }
            return interactiveObject.transform.position + viewOffset;
        }

        private void OnInteractiveObjectDetected(InteractiveObjectDetectedSignal signal)
        {
            var viewWorldPosition = GetViewWorldPosition(signal.InteractiveObject);
            _model = new EnvironmentInteractionModel(signal.InteractiveObject.BaseParams.Name, signal.InteractiveObject.BaseParams.InteractionDescription, viewWorldPosition);

            _view.SetActive(true);
            _view.SetInteractiveObjectName(_model.InteractiveObjectName.GetLocalizedString());

            ShowCurrentInteractiveObjectControlsTips();
        }

        private void OnInteractiveObjectLost(InteractiveObjectLostSignal signal) => _view.SetActive(false);

        private void OnControlsChanged(ControlsChangedSignal signal)
        {

        }
    }
}