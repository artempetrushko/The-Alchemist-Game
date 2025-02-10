using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class InventoryItemsCategoryButton : MonoBehaviour
    {
        [SerializeField]
        private Button defaultButtonState;
        [SerializeField]
        private Image defaultButtonIcon;
        [SerializeField]
        private Image pressedButtonState;
        [SerializeField]
        private Image pressedButtonIcon;

        public void SetInfo(Sprite categoryIcon, UnityAction buttonPressedAction)
        {
            defaultButtonIcon.sprite = pressedButtonIcon.sprite = categoryIcon;
            defaultButtonState.onClick.AddListener(buttonPressedAction);
        }

        public void SetButtonState(bool isPressed)
        {
            defaultButtonState.gameObject.SetActive(!isPressed);
            pressedButtonState.gameObject.SetActive(isPressed);
        }
    }
}