using UnityEngine;
using UnityEngine.Events;

namespace UI.PlayerMenu
{
    public class ItemCellActionsPanelController
    {
        private readonly Vector2 viewOffset = new(Screen.width * 0.15f, Screen.height * 0.15f);

        private ItemCellActionsPanelView _itemCellActionsPanelView;
        private ItemCellActionButton actionButtonPrefab;

        public ItemCellActionsPanelController(ItemCellActionsPanelView itemCellActionsPanelView)
        {
            _itemCellActionsPanelView = itemCellActionsPanelView;
        }
    }

    public class ItemCellActionsPanelView : MonoBehaviour
    {
        public int ActionButtonsCount => GetComponentsInChildren<ItemCellActionButton>().Length;

        public void CreateActionButtons((string actionDescription, Sprite icon, UnityAction action)[] actionDatas)
        {
            foreach (var actionData in actionDatas)
            {
                var actionButton = Instantiate(actionButtonPrefab, transform);
                actionButton.SetInfo(actionData.icon, actionData.actionDescription, actionData.action);
            }
        }

        public void SetPosition(ItemCellView linkedItemCell)
        {
            var itemCellPosition = linkedItemCell.transform.position;
            var itemCellRect = linkedItemCell.GetComponent<RectTransform>().rect;
            var actionMenuRect = GetComponent<RectTransform>().rect;

            transform.position = itemCellPosition;
            if (Screen.width - (itemCellPosition.x + itemCellRect.width / 2) > actionMenuRect.width + viewOffset.x)
            {
                transform.position += new Vector3(viewOffset.x, itemCellRect.height, 0);
            }
            else if (itemCellPosition.x - itemCellRect.width / 2 > actionMenuRect.width + viewOffset.x)
            {
                transform.position += new Vector3(-viewOffset.x, itemCellRect.height, 0);
            }
            else if (Screen.height - (itemCellPosition.y + itemCellRect.height / 2) > actionMenuRect.height + viewOffset.y)
            {
                transform.position += new Vector3(0, viewOffset.y, 0);
            }
            else transform.position += new Vector3(0, -viewOffset.y, 0);
        }

        public void SelectActionButton(int buttonNumber) => transform.GetChild(buttonNumber - 1).GetComponent<ItemCellActionButton>().Select();

        public void ClickActionButton(int buttonNumber) => transform.GetChild(buttonNumber - 1).GetComponent<ItemCellActionButton>().Click();
    }
}