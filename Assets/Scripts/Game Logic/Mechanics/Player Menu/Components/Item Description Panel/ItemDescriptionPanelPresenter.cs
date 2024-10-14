using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameLogic.LootSystem;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class ItemDescriptionPanelPresenter : IDisposable
    {
        private const float PANEL_APPEARANCE_LATENCY_IN_SECONDS = 1f;

        private ItemDescriptionPanelView _view;
        private SignalBus _signalBus;

        public ItemDescriptionPanelPresenter(ItemDescriptionPanelView view, SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;

            _signalBus.Subscribe<FilledItemSlotPointerEnterSignal>(OnFilledSlotPointerEnter);
            _signalBus.Subscribe<FilledItemSlotPointerExitSignal>(OnFilledSlotPointerExit);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FilledItemSlotPointerEnterSignal>(OnFilledSlotPointerEnter);
            _signalBus.Unsubscribe<FilledItemSlotPointerExitSignal>(OnFilledSlotPointerExit);
        }

        private async UniTask ShowPanelAsync(ItemSlot selectedItemSlot)
        {
            _view.SetActive(true);

            var panelPosition = selectedItemSlot.GetAdditionalPanelPosition(_view.Rect);
            _view.SetPosition(panelPosition);

            SetPanelContent(selectedItemSlot.ContainedItem);

            await UniTask.WaitForSeconds(PANEL_APPEARANCE_LATENCY_IN_SECONDS);

            _view.SetVisible(true);
        }

        private void SetPanelContent(ItemState item)
        {
            _view.SetItemTitleText(item.Title.GetLocalizedString());
            _view.SetItemDescriptionText(item.Description.GetLocalizedString());

            var namedItemParams = item.GetType().GetProperties()
                .Where(property => typeof(IFormattedItemParameter).IsAssignableFrom(property.PropertyType))
                .Select(property => property.GetValue(item) as IFormattedItemParameter)
                .ToList();
            for (var i = 0; i < namedItemParams.Count; i++)
            {
                var (paramName, formattedParamValue) = namedItemParams[i].GetFormattedParamInfo();

                var parameterView = _view.GetOrCreateParameterViewByIndex(i);
                parameterView.SetActive(true);
                parameterView.SetParameterTitleText(paramName);
                parameterView.SetParameterValueText(formattedParamValue);
                //TODO: доделать
            }
        }

        private void OnFilledSlotPointerEnter(FilledItemSlotPointerEnterSignal signal) => ShowPanelAsync(signal.ItemSlot).Forget();

        private void OnFilledSlotPointerExit(FilledItemSlotPointerExitSignal signal)
        {
            _view.DisableAllParameterViews();
            _view.SetVisible(false);
            _view.SetActive(false);
        }
    }
}