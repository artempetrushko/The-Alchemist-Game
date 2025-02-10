using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.LootSystem
{
    [RequireComponent(typeof(EventTrigger))]
    public class ContainedItemView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text itemTitle;
        [SerializeField]
        private TMP_Text itemDescription;
        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private GameObject itemsCounter;
        [SerializeField]
        private Button buttonComponent;
        [Space, SerializeField]
        private EventTrigger eventTrigger;

        public void SetInfo(ItemState item, UnityAction viewPressedAction)
        {
            itemTitle.text = item.BaseParams.Title;
            itemDescription.text = item.Description;
            itemIcon.sprite = item.BaseParams.Icon;
            if (item is StackableItemState stackableItem)
            {
                itemsCounter.SetActive(true);
                itemsCounter.GetComponentInChildren<TMP_Text>().text = "x" + stackableItem.ItemsCount.ToString();
            }
            buttonComponent.onClick.AddListener(viewPressedAction);
        }

        public void Select() => buttonComponent.Select();

        public void AddEventTriggerListener(EventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            var requiredEntry = eventTrigger.triggers.FirstOrDefault(entry => entry.eventID == triggerType);
            if (requiredEntry == null)
            {
                requiredEntry = new EventTrigger.Entry() { eventID = triggerType };
                eventTrigger.triggers.Add(requiredEntry);
            }
            requiredEntry.callback.AddListener((eventData) => action(eventData));
        }
    }
}