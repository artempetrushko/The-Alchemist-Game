using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace UI.PlayerMenu
{
    public class ItemDescriptionPanelController
    {
        private const float PANEL_APPEARANCE_LATENCY_IN_SECONDS = 1f;
        private const float PANEL_POSITION_OFFSET_COEF_X = 0.15f;
        private const float PANEL_POSITION_OFFSET_COEF_Y = 0.3f;

        private ItemDescriptionPanelView _itemDescriptionPanelView;
        private ItemParameterView _parameterViewPrefab;
        private DiContainer _diContainer;

        public ItemDescriptionPanelController(DiContainer diContainer, ItemDescriptionPanelView itemDescriptionPanelView, ItemParameterView itemParameterViewPrefab)
        {
            _diContainer = diContainer;
            _itemDescriptionPanelView = itemDescriptionPanelView;
            _parameterViewPrefab = itemParameterViewPrefab;
        }

        public async UniTask ShowPanel(ItemState item, ItemCellView linkedItemCell)
        {
            SetPanelContent(item);
            SetPanelPosition(linkedItemCell.transform);

            await UniTask.WaitForSeconds(PANEL_APPEARANCE_LATENCY_IN_SECONDS);

            _itemDescriptionPanelView.Show();
        }

        public void HidePanel()
        {
            _itemDescriptionPanelView.Hide();
        }

        private void SetPanelContent(ItemState item)
        {
            _itemDescriptionPanelView.SetItemTitleText(item.BaseParams.Title);
            _itemDescriptionPanelView.SetItemDescriptionText(item.Description);

            foreach (var param in item.GetItemParams())
            {
                var parameterView = _diContainer
                    .InstantiatePrefab(_parameterViewPrefab, _itemDescriptionPanelView.ParametersContainer.transform)
                    .GetComponent<ItemParameterView>();
                parameterView.SetParameterTitleText(param.Key);
                parameterView.SetParameterValueText(param.Value);
            }
        }

        private void SetPanelPosition(Transform linkedItemCell)
        {
            Canvas.ForceUpdateCanvases();
            var itemCellPosition = linkedItemCell.position;
            var itemCellRect = linkedItemCell.GetComponent<RectTransform>().rect;
            var viewRect = _itemDescriptionPanelView.Rect;

            Vector2 offsetVector;
            if (Screen.width - (itemCellPosition.x + itemCellRect.width / 2) > viewRect.width + PANEL_POSITION_OFFSET_COEF_X)
            {
                offsetVector = new Vector2(PANEL_POSITION_OFFSET_COEF_X, itemCellRect.height);
            }
            else if (itemCellPosition.x - itemCellRect.width / 2 > viewRect.width + PANEL_POSITION_OFFSET_COEF_X)
            {
                offsetVector = new Vector2(-PANEL_POSITION_OFFSET_COEF_X, itemCellRect.height);
            }
            else if (Screen.height - (itemCellPosition.y + itemCellRect.height / 2) > viewRect.height + PANEL_POSITION_OFFSET_COEF_Y)
            {
                offsetVector = new Vector2(0, PANEL_POSITION_OFFSET_COEF_Y);
            }
            else
            {
                offsetVector = new Vector2(0, -PANEL_POSITION_OFFSET_COEF_Y);
            }
            _itemDescriptionPanelView.SetPosition(itemCellPosition + (Vector3)offsetVector);
        }
    }
}