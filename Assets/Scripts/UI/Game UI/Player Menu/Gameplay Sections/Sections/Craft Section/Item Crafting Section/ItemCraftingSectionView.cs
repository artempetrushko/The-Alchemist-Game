using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCraftingSectionView : MonoBehaviour
{
    [SerializeField]
    private CraftingItemTemplateView craftingTemplateView;
    [SerializeField]
    private Image craftProgressRound;
    [SerializeField]
    private GameObject energyCellsContainer;
    [SerializeField]
    private EventTrigger pointerEventTrigger;

    public IngredientCellView[] IngredientCells => craftingTemplateView.IngredientCells;
    public ItemCellView[] EnergyCells => energyCellsContainer.GetComponentsInChildren<ItemCellView>();

    public void CreateNewCraftTemplate(Sprite craftingItemIcon, IngredientCellsTemplateView ingredientCellsTemplate)
    {
        ClearCraftTemplate();
        craftingTemplateView.SetInfo(craftingItemIcon, ingredientCellsTemplate);
    }

    public void ClearCraftTemplate() => craftingTemplateView.Clear();

    public void FillCraftProgressBar(float fillAmount)
    {
        if (!craftProgressRound.gameObject.activeInHierarchy)
        {
            craftProgressRound.gameObject.SetActive(true);
        }      
        craftProgressRound.fillAmount = fillAmount;      
    }

    public void HideCraftProgressBar()
    {
        craftProgressRound.fillAmount = 0f;
        craftProgressRound.gameObject.SetActive(false);
    }

    public void AddEventTriggerListener(EventTriggerType triggerType, UnityAction<BaseEventData> action)
    {
        var requiredEntry = pointerEventTrigger.triggers.FirstOrDefault(entry => entry.eventID == triggerType);
        if (requiredEntry == null)
        {
            requiredEntry = new EventTrigger.Entry() { eventID = triggerType };
            pointerEventTrigger.triggers.Add(requiredEntry);
        }
        requiredEntry.callback.AddListener((eventData) => action(eventData));
    }
}
