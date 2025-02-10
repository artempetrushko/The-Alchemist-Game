using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class ItemCellActionButton : MonoBehaviour
    {
        [SerializeField]
        private Image actionIcon;
        [SerializeField]
        private TMP_Text actionTitle;
        [SerializeField]
        private Button buttonComponent;

        public void SetInfo(Sprite actionIcon, string actionTitle, UnityAction buttonPressedAction)
        {
            this.actionIcon.sprite = actionIcon;
            this.actionTitle.text = actionTitle;
            buttonComponent.onClick.AddListener(buttonPressedAction);
        }

        public void Select() => buttonComponent.Select();

        public void Click() => buttonComponent.onClick.Invoke();
    }
}