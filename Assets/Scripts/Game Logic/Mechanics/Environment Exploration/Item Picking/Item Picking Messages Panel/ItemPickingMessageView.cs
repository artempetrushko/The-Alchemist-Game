using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.EnvironmentExploration
{
    public class ItemPickingMessageView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemsCountText;
        [SerializeField] private Image _itemIcon;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetAsFirstSibling() => transform.SetAsFirstSibling();

        public void SetItemIcon(Sprite icon) => _itemIcon.sprite = icon;

        public void SetItemsCountText(string text) => _itemsCountText.text = text;
    }
}