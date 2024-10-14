using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class MovingItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<PointerEventData> DraggingStarted;
        public event Action<PointerEventData> DraggingStay;
        public event Action<PointerEventData> DraggingFinished;

        [SerializeField] private Image _itemIcon;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void OnBeginDrag(PointerEventData eventData) => DraggingStarted?.Invoke(eventData);

        public void OnDrag(PointerEventData eventData) => DraggingStay?.Invoke(eventData);

        public void OnEndDrag(PointerEventData eventData) => DraggingFinished?.Invoke(eventData);

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetLocalScale(Vector3 localScale) => transform.localScale = localScale;

        public void SetItemIcon(Sprite icon) => _itemIcon.sprite = icon;
    }
}