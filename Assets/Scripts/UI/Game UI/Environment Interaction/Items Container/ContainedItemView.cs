using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class ContainedItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemTitle;
    [SerializeField] private TMP_Text _itemDescription;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private GameObject _itemsCounter;
    [SerializeField] private Button _buttonComponent;
    [SerializeField] private EventTrigger _eventTrigger;

    public void SetInfo(ItemState item, UnityAction viewPressedAction)
    {
        _itemTitle.text = item.BaseParams.Title;
        _itemDescription.text = item.Description;
        _itemIcon.sprite = item.BaseParams.Icon;
        if (item is StackableItemState stackableItem)
        {
            _itemsCounter.SetActive(true);
            _itemsCounter.GetComponentInChildren<TMP_Text>().text = "x" + stackableItem.ItemsCount.ToString();
        }
        _buttonComponent.onClick.AddListener(viewPressedAction);
    }

    public void Select() => _buttonComponent.Select();

    public void AddEventTriggerListener(EventTriggerType triggerType, UnityAction<BaseEventData> action)
    {
        var requiredEntry = _eventTrigger.triggers.FirstOrDefault(entry => entry.eventID == triggerType);
        if (requiredEntry == null)
        {
            requiredEntry = new EventTrigger.Entry() { eventID = triggerType };
            _eventTrigger.triggers.Add(requiredEntry);
        }
        requiredEntry.callback.AddListener((eventData) => action(eventData));
    }
}
