using System;
using LeTai.TrueShadow;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
	public class ItemSlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDropHandler
	{
		public event Action<PointerEventData> PointerEnter;
		public event Action<PointerEventData> PointerExit;
		public event Action<PointerEventData> PointerDown;
        public event Action<PointerEventData> DraggingObjectDropped;

		[SerializeField] private Image _itemIcon; 
		[SerializeField] private TrueShadow _backgroundShadow;
		[Space]
		[SerializeField] private ItemSlotModule[] _cellModules;

		public Vector3 ItemIconPosition => _itemIcon.transform.position;

        public void OnPointerEnter(PointerEventData eventData) => PointerEnter?.Invoke(eventData);

        public void OnPointerExit(PointerEventData eventData) => PointerExit?.Invoke(eventData);

        public void OnPointerDown(PointerEventData eventData) => PointerDown?.Invoke(eventData);

        public void OnDrop(PointerEventData eventData) => DraggingObjectDropped?.Invoke(eventData);

        public void SetItemIconActive(bool isActive) => _itemIcon.gameObject.SetActive(isActive);

		public void SetItemIcon(Sprite icon) => _itemIcon.sprite = icon;

		public void SetAllModulesActive(bool isActive)
		{
			foreach (var module in _cellModules)
			{
				module.SetActive(isActive);
			}
		}

		public void SetBackgroundShadowActive(bool isActive) => _backgroundShadow.enabled = isActive;

		public void SetBackgroundShadowColor(Color color) => _backgroundShadow.Color = color;
    }
}