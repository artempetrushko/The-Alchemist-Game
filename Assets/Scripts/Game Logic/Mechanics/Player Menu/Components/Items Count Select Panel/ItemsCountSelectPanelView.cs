using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class ItemsCountSelectPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _actionDescriptionText;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemsCounterText;	
        [SerializeField] private TMP_Text _itemsMinCountText;
        [SerializeField] private TMP_Text _itemsMaxCountText;
        [SerializeField] private Slider _itemsCounterSlider;
        [SerializeField] private ActionButton _selectItemsCountActionButton;
        [SerializeField] private ActionButton _selectAllItemsActionButton;
        [SerializeField] private ActionButton _cancelActionButton;

        public Slider ItemsCounterSlider => _itemsCounterSlider;
        public ActionButton SelectItemsCountActionButton => _selectItemsCountActionButton;
        public ActionButton SelectAllItemsActionButton => _selectAllItemsActionButton;
        public ActionButton CancelActionButton => _cancelActionButton;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetActionDescriptionText(string text) => _actionDescriptionText.text = text;

        public void SetItemIcon(Sprite icon) => _itemIcon.sprite = icon;

        public void SetItemsCounterText(string text) => _itemsCounterText.text = text;

        public void SetItemsMinCountText(string text) => _itemsMinCountText.text = text;

        public void SetItemsMaxCountText(string text) => _itemsMaxCountText.text = text;
    }
}