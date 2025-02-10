using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.LootSystem;
using LeTai.TrueShadow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameLogic.PlayerMenu
{
    [RequireComponent(typeof(EventTrigger))]
    public class ItemCellView : MonoBehaviour
    {
        public event Action CellSelected;
        public event Action CellDeselected;

        [SerializeField]
        protected ItemCellModuleView[] cellModules;
        [SerializeField]
        protected TrueShadow backgroundShadow;
        [SerializeField]
        protected Color selectedStateColor;
        [Space, SerializeField]
        protected ItemView itemViewPrefab;
        [SerializeField]
        protected GameObject itemViewContainer;
        [Space, SerializeField]
        protected EventTrigger eventTrigger;

        protected CellsSubsectionNavigation parentSubsection;
        protected List<ItemCellModuleView> currentCellModules = new();

        public ItemSlot LinkedItemSlot { get; set; }
        public GameObject ItemViewContainer => itemViewContainer;

        public void OnCellSelected() => CellSelected?.Invoke();

        public void OnCellDeselected() => CellDeselected?.Invoke();

        public void EnableAndUpdateCellModules(ItemState newInventoryItem)
        {
            currentCellModules.Clear();
            foreach (var cellModule in cellModules)
            {
                if (cellModule.TryEnableWithNewItem(newInventoryItem))
                {
                    cellModule.UpdateContent(newInventoryItem);
                    currentCellModules.Add(cellModule);
                }
                else
                {
                    cellModule.SetActive(false);
                }
            }
        }

        public void EnableCellModules() => currentCellModules.ForEach(cellModule => cellModule.SetActive(true));

        public void DisableCellModules() => cellModules.ToList().ForEach(cellModule => cellModule.SetActive(false));

        public virtual void SetAppearance(bool isCellSelected)
        {
            backgroundShadow.enabled = isCellSelected;
            if (backgroundShadow.enabled)
            {
                backgroundShadow.Color = selectedStateColor;
            }
        }

        public void PlaceItemView(ItemState item)
        {
            if (item.ItemView == null)
            {
                item.ItemView = Instantiate(itemViewPrefab, itemViewContainer.transform);
            }
            else
            {
                item.ItemView.transform.SetParent(itemViewContainer.transform);
            }
            SubscribeItemViewDraggingEvents(item.ItemView.GetComponent<ItemViewDraggingModule>());
            AdjustItemViewTransform(item.ItemView);
        }

        public void PlaceItemViewCopy(ItemState item)
        {
            if (item.HUDItemView == null)
            {
                item.HUDItemView = Instantiate(itemViewPrefab, itemViewContainer.transform);
            }
            else
            {
                item.HUDItemView.transform.SetParent(itemViewContainer.transform);
            }
            AdjustItemViewTransform(item.HUDItemView);
        }

        public void ClearItemView()
        {
            if (itemViewContainer.transform.childCount > 0)
            {
                Destroy(itemViewContainer.transform.GetChild(0).gameObject);
            }
        }

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

        protected void OnEnable()
        {
            parentSubsection = GetComponentInParent<CellsSubsectionNavigation>();
            if (parentSubsection != null)
            {
                AddEventTriggerListener(EventTriggerType.PointerEnter, (eventData) => { parentSubsection.SelectItemCellByPointer(this); });
            }
            DisableCellModules();
        }

        private void OnDisable()
        {
            OnCellDeselected();
        }

        protected void OnDestroy()
        {
            CellSelected = CellDeselected = null;
        }

        protected void AdjustItemViewTransform(ItemView itemView)
        {
            itemView.transform.localPosition = Vector3.zero;
            itemView.transform.localScale = Vector3.one;
            itemView.AdjustViewSize(itemViewContainer.GetComponent<RectTransform>().rect.size);
        }

        protected void SubscribeItemViewDraggingEvents(ItemViewDraggingModule draggingModule)
        {
            draggingModule.ClearDraggingEventsSubscriptions();
            draggingModule.DraggingStarted += DisableCellModules;
            draggingModule.DraggingFailed += EnableCellModules;
        }
    }
}