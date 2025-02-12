using System;
using Cysharp.Threading.Tasks;
using EventBus;
using GameLogic.LootSystem;
using Zenject;

namespace GameLogic.EnvironmentExploration
{
    public class ItemPickingMessagesPanelPresenter : IDisposable
    {
        private ItemPickingMessagesPanelConfig _config;
        private ItemPickingMessagesPanelView _view;
        private SignalBus _signalBus;

        private bool _isShowingStarted = false;
        private int _currentShowingTime = 0;

        public ItemPickingMessagesPanelPresenter(ItemPickingMessagesPanelConfig config, ItemPickingMessagesPanelView view, SignalBus signalBus)
        {
            _config = config;
            _view = view;
            _signalBus = signalBus;

            _signalBus.Subscribe<ItemPickedSignal>(OnItemPicked);

            CreateItemPickingMessageViews();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ItemPickedSignal>(OnItemPicked);
        }

        private void CreateItemPickingMessageViews()
        {
            for (var i = 0; i < _config.MaxMessagesCount; i++)
            {
                var itemPickingMessageView = _view.CreateItemPickingMessageView();
                itemPickingMessageView.SetActive(false);
            }
        }

        private void ShowNewMessage(Item pickedItem)
        {
            var availableItemPickingMessageView = _view.GetInactiveItemPickingMessageView();
            if (availableItemPickingMessageView != null)
            {
                availableItemPickingMessageView.SetActive(true);
            }
            else
            {
                availableItemPickingMessageView = _view.GetItemPickingMessageViewByIndex(_config.MaxMessagesCount - 1);
                availableItemPickingMessageView.SetAsFirstSibling();
            }

            availableItemPickingMessageView.SetItemIcon(pickedItem.Icon);

            var itemsCount = pickedItem is StackableItem stackableItem
                ? stackableItem.Count.Value
                : 1;
            availableItemPickingMessageView.SetItemsCountText($"+{itemsCount}");
        }

        private async UniTask StartMessagesShowingTimerAsync()
        {
            _isShowingStarted = true;

            while (_currentShowingTime < _config.MessagesShowingTimeInSeconds)
            {
                await UniTask.WaitForSeconds(1f);
                _currentShowingTime++;
            }

            _view.DisableAllItemPickingMessageViews();
            _isShowingStarted = false;
        }

        private void OnItemPicked(ItemPickedSignal signal)
        {
            ShowNewMessage(signal.Item);

            _currentShowingTime = 0;
            if (!_isShowingStarted)
            {
                StartMessagesShowingTimerAsync().Forget();
            }
        }
    }
}