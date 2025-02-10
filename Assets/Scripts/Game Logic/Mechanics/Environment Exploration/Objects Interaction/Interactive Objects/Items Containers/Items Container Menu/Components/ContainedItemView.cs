using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.EnvironmentExploration
{
    [RequireComponent(typeof(EventTrigger))]
    public class ContainedItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<PointerEventData, ContainedItemView> PointerEnter;
        public event Action<PointerEventData, ContainedItemView> PointerExit;

        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemDescription;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemsCounter;
        [SerializeField] private Button _buttonComponent;

        public Button ButtonComponent => _buttonComponent;

        public void OnPointerEnter(PointerEventData eventData) => PointerEnter?.Invoke(eventData, this);

        public void OnPointerExit(PointerEventData eventData) => PointerExit?.Invoke(eventData, this);

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetItemNameText(string text) => _itemName.text = text;

        public void SetItemDescriptionText(string text) => _itemDescription.text = text;

        public void SetItemIcon(Sprite icon) => _itemIcon.sprite = icon;

        public void SetItemsCounterActive(bool isActive) => _itemsCounter.gameObject.SetActive(isActive);

        public void SetItemsCounterText(string text) => _itemsCounter.text = text; 
    }
}