using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerMenu
{
    public class ControlTipView : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _keyName;
        [SerializeField] protected GameObject _gamepadKeyIconContainer;
        [SerializeField] protected Image _gamepadKeyIcon;
        [SerializeField] protected Image _keyIconContainer;

        public void SetKeyIconContainerActive(bool isActive) => _keyIconContainer.gameObject.SetActive(isActive);

        public void SetGamepadKeyIconContainerActive(bool isActive) => _gamepadKeyIconContainer.gameObject.SetActive(isActive);

        public void SetGamepadKeyIcon(Sprite icon) => _gamepadKeyIcon.sprite = icon;

        public void SetKeyNameText(string text) => _keyName.text = text;

        public void SetInfo(ControlTip actionTip)
        {
            ToggleViewElements(actionTip.KeyIcon != null);
            if (actionTip.KeyIcon != null)
            {
                _gamepadKeyIconContainer.GetComponentInChildren<Image>().sprite = actionTip.KeyIcon;
            }
            else
            {
                _keyName.text = actionTip.KeyName;
            }
        }

        protected void ToggleViewElements(bool isKeyIconAvailable)
        {
            _keyIconContainer.gameObject.SetActive(!isKeyIconAvailable);
            _gamepadKeyIconContainer.SetActive(isKeyIconAvailable);
        }
    }
}