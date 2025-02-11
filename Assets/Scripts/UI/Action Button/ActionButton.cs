using Controls;
using EventBus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace GameLogic
{
    public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _buttonComponent;
        [SerializeField] private DetailedControlTipView _controlTipView;
        [SerializeField] private Color _normalStateContentColor;
        [SerializeField] private Color _selectedStateContentColor;

        private SignalBus _signalBus;

        public Button ButtonComponent => _buttonComponent;
        public DetailedControlTipView ControlTipView => _controlTipView;

        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable() => _signalBus.Fire(new ActionButtonControlTipColorChangedSignal(_selectedStateContentColor));

        public void OnPointerEnter(PointerEventData eventData) => _signalBus.Fire(new ActionButtonControlTipColorChangedSignal(_selectedStateContentColor));

        public void OnPointerExit(PointerEventData eventData) => _signalBus.Fire(new ActionButtonControlTipColorChangedSignal(_normalStateContentColor));

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}