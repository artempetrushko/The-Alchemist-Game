using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemSlotsController : IDisposable
    {
        public static ItemSlot ItemMovingStartSlot;

        private const float MOVING_ITEM_VIEW_SCALE = 1.15f;

        private MovingItemView _movingItemView;
        private SignalBus _signalBus;

        public ItemSlotsController(MovingItemView movingItemView, SignalBus signalBus)
        {
            _movingItemView = movingItemView;
            _signalBus = signalBus;

            _movingItemView.DraggingStarted += OnDraggingStarted;
            _movingItemView.DraggingStay += OnDraggingStay;
            _movingItemView.DraggingFinished += OnDraggingFinished;

            _signalBus.Subscribe<ItemDraggingRequestedSignal>(OnItemDraggingRequested);
            _signalBus.Subscribe<ItemMovingStartedSignal>(OnItemMovingStarted);
            _signalBus.Subscribe<ItemMovingFinishedSignal>(OnItemMovingFinished);
        }

        public void Dispose()
        {
            _movingItemView.DraggingStarted -= OnDraggingStarted;
            _movingItemView.DraggingStay -= OnDraggingStay;
            _movingItemView.DraggingFinished -= OnDraggingFinished;

            _signalBus.Unsubscribe<ItemDraggingRequestedSignal>(OnItemDraggingRequested);
            _signalBus.Unsubscribe<ItemMovingStartedSignal>(OnItemMovingStarted);
            _signalBus.Unsubscribe<ItemMovingFinishedSignal>(OnItemMovingFinished);
        }

        private void SetCursorActive(bool isActive)
        {
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isActive;
        }

        private void OnItemDraggingRequested(ItemDraggingRequestedSignal signal)
        {
            if (signal.SelectedSlot.ContainedItem != null)
            {
                ItemMovingStartSlot = signal.SelectedSlot;

                _movingItemView.SetActive(true);
                _movingItemView.SetPosition(ItemMovingStartSlot.View.ItemIconPosition);
                _movingItemView.SetItemIcon(ItemMovingStartSlot.ContainedItem.Icon);
            }
        }

        private void OnDraggingStarted(PointerEventData eventData) => _movingItemView.SetActive(true);

        private void OnDraggingStay(PointerEventData eventData) => _movingItemView.SetPosition(Input.mousePosition);

        private void OnDraggingFinished(PointerEventData eventData)
        {
            _movingItemView.SetActive(false);
            ItemMovingStartSlot = null;

            _signalBus.Fire<ItemDraggingFinishedSignal>();
        }

        private void OnItemMovingStarted(ItemMovingStartedSignal signal)
        {
            ItemMovingStartSlot = signal.StartItemSlot;
            ItemMovingStartSlot.SetContentVisible(false);

            SetCursorActive(false);
            _movingItemView.SetActive(true);
            _movingItemView.SetPosition(ItemMovingStartSlot.View.ItemIconPosition);
            _movingItemView.SetLocalScale(new Vector3(MOVING_ITEM_VIEW_SCALE, MOVING_ITEM_VIEW_SCALE, 1));
            _movingItemView.SetItemIcon(ItemMovingStartSlot.ContainedItem.Icon);

            _signalBus.Subscribe<SelectedItemSlotChangedSignal>(OnSelectedItemSlotChanged);
        }

        private void OnSelectedItemSlotChanged(SelectedItemSlotChangedSignal signal) => _movingItemView.transform.position = signal.SelectedItemSlot.View.ItemIconPosition;

        private void OnItemMovingFinished(ItemMovingFinishedSignal signal)
        {
            signal.DestinationItemSlot.TryPlaceItemFromOtherSlot(ItemMovingStartSlot);

            _movingItemView.SetActive(false);

            ItemMovingStartSlot = null;
            SetCursorActive(true);

            _signalBus.Unsubscribe<SelectedItemSlotChangedSignal>(OnSelectedItemSlotChanged);
        }
    }
}