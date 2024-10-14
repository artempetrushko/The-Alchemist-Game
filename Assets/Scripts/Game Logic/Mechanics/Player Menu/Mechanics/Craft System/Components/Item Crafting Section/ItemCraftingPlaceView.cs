using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu.Craft
{
    public class ItemCraftingPlaceView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<PointerEventData> PointerDown;
        public event Action<PointerEventData> PointerUp;

        [SerializeField] private Image _craftingItemIcon;
        [SerializeField] private GameObject _ingredientCellsTemplatesContainer;

        public void OnPointerDown(PointerEventData eventData) => PointerDown?.Invoke(eventData);

        public void OnPointerUp(PointerEventData eventData) => PointerUp?.Invoke(eventData);

        public IngredientSlotsTemplateView CreateIngredientSlotsTemplate(IngredientSlotsTemplateView ingredientCellsTemplatePrefab) => Instantiate(ingredientCellsTemplatePrefab, _ingredientCellsTemplatesContainer.transform);


        public void SetCraftingItemIconActive(bool isActive) => _craftingItemIcon.gameObject.SetActive(isActive);

        public void SetCraftingItemIcon(Sprite icon) => _craftingItemIcon.sprite = icon;
    }
}