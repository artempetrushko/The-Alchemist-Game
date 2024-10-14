using GameLogic.LootSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemSlot : IPlayerMenuInteractable
    {
        private const float PANEL_POSITION_OFFSET_COEF_X = 0.15f;
        private const float PANEL_POSITION_OFFSET_COEF_Y = 0.3f;

        private ItemSlotView _view;
        private ItemState _containedItem;
        private SignalBus _signalBus;

        public ref ItemState ContainedItem => ref _containedItem;
        public ItemSlotView View
        {
            get => _view;
            set
            {
                if (value != _view)
                {
                    if (_view != null)
                    {
                        _view.PointerEnter -= OnItemSlotPointerEnter;
                        _view.PointerExit -= OnItemSlotPointerExit;
                        _view.PointerDown -= OnItemSlotPointerDown;
                        _view.DraggingObjectDropped -= OnDraggingItemDropped;
                    }
                    _view = value;
                    if (_view != null)
                    {
                        _view.PointerEnter += OnItemSlotPointerEnter;
                        _view.PointerExit += OnItemSlotPointerExit;
                        _view.PointerDown += OnItemSlotPointerDown;
                        _view.DraggingObjectDropped += OnDraggingItemDropped;
                    }
                }
            }
        }
        public ItemSlotView LinkedHUDView { get; set; }
        public ItemSlotNeighboursInfo ItemSlotNeighboursInfo { get; set; }
        public NeighboringInteractableElements Neighbours { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public ItemSlotInteractionsConfig InteractionsConfig { get; set; }

        public ItemSlot(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Select() => View.SetBackgroundShadowActive(true);

        public void Deselect() => View.SetBackgroundShadowActive(false);

        public void Click()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool TryPlaceNewItem(ItemState item)
        {
            if (_containedItem != null)
            {
                return false;
            }
            PlaceNewItem(item);
            return true;
        }

        public virtual void PlaceNewItem(ItemState item)
        {
            _containedItem = item;

            View.SetItemIconActive(true);
            View.SetItemIcon(item.Icon);
            if (LinkedHUDView != null)
            {
                LinkedHUDView.SetItemIconActive(true);
                LinkedHUDView.SetItemIcon(item.Icon);
            }
        }

        public virtual bool TryPlaceItemFromOtherSlot(ItemSlot otherSlot)
        {
            if (ContainedItem == null)
            {
                PlaceNewItem(otherSlot.ContainedItem);
                otherSlot.Clear();
                return true;
            }
            if (otherSlot.ContainedItem is not StackableItemState || ContainedItem.Id != otherSlot.ContainedItem.Id)
            {
                return TrySwapItems(otherSlot);
            }
            
            var containedStackableItem = ContainedItem as StackableItemState;
            var otherStackableItem = otherSlot.ContainedItem as StackableItemState;
            if (containedStackableItem.Count.Value == containedStackableItem.Count.MaxValue)
            {
                return false;
            }
            containedStackableItem.Count.Value += otherStackableItem.Count.Value;
            if (containedStackableItem.Count.Value <= containedStackableItem.Count.MaxValue)
            {
                otherSlot.Clear();
            }
            else
            {
                otherStackableItem.Count.Value = containedStackableItem.Count.Value - containedStackableItem.Count.MaxValue;
                containedStackableItem.Count.Value = containedStackableItem.Count.MaxValue;
            }
            return true;
        }

        public bool TrySwapItems(ItemSlot otherSlot)
        {
            if (otherSlot is ISpecialItemSlot specialSlot && !specialSlot.CheckItemRequirementsCompliance(ContainedItem))
            {
                return false;
            }

            ref var otherSlotItem = ref otherSlot.ContainedItem;
            otherSlot.PlaceNewItem(ContainedItem);
            PlaceNewItem(otherSlotItem);

            return true;
        }  

        public void SetContentVisible(bool isVisible)
        {
            View.SetItemIconActive(isVisible);
            View.SetAllModulesActive(isVisible);
        }

        public void Clear()
        {

        } 

        public Vector3 GetAdditionalPanelPosition(Rect additionalPanelRect)
        {
            var itemSlotPosition = View.transform.position;
            var itemSlotRect = View.GetComponent<RectTransform>().rect;

            Vector2 offsetVector;
            if (Screen.width - (itemSlotPosition.x + itemSlotRect.width / 2) > additionalPanelRect.width + PANEL_POSITION_OFFSET_COEF_X)
            {
                offsetVector = new Vector2(PANEL_POSITION_OFFSET_COEF_X, itemSlotRect.height);
            }
            else if (itemSlotPosition.x - itemSlotRect.width / 2 > additionalPanelRect.width + PANEL_POSITION_OFFSET_COEF_X)
            {
                offsetVector = new Vector2(-PANEL_POSITION_OFFSET_COEF_X, itemSlotRect.height);
            }
            else if (Screen.height - (itemSlotPosition.y + itemSlotRect.height / 2) > additionalPanelRect.height + PANEL_POSITION_OFFSET_COEF_Y)
            {
                offsetVector = new Vector2(0, PANEL_POSITION_OFFSET_COEF_Y);
            }
            else
            {
                offsetVector = new Vector2(0, -PANEL_POSITION_OFFSET_COEF_Y);
            }

            return itemSlotPosition + (Vector3)offsetVector;
        }

        private void OnItemSlotPointerEnter(PointerEventData eventData)
        {
            if (ContainedItem != null)
            {
                _signalBus.Fire(new FilledItemSlotPointerEnterSignal(this));
            }
        }

        private void OnItemSlotPointerExit(PointerEventData eventData) => _signalBus.Fire(new FilledItemSlotPointerExitSignal(this));

        private void OnItemSlotPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right && ContainedItem != null)
            {
                _signalBus.Fire(new FilledItemSlotPointerDownSignal(this));
            }
        }

        private void OnDraggingItemDropped(PointerEventData eventData)
        {
            if (ItemSlotsController.ItemMovingStartSlot != null)
            {
                TryPlaceItemFromOtherSlot(ItemSlotsController.ItemMovingStartSlot);
            }
        }
    }
}