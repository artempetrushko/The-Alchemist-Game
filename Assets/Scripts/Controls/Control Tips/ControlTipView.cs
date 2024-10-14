using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controls
{
    public class ControlTipView : MonoBehaviour
    {
        [SerializeField] private Image _keyIconContainer;
        [SerializeField] private TMP_Text _keyName;
        [SerializeField] private GameObject _gamepadKeyIconContainer;
        [SerializeField] private Image _gamepadKeyIcon; 

        public void SetKeyIconContainerActive(bool isActive) => _keyIconContainer.gameObject.SetActive(isActive);

        public void SetKeyNameText(string text) => _keyName.text = text;

        public void SetKeyIconContainerColor(Color color) => _keyIconContainer.color = color;

        public void SetKeyNameColor(Color color) => _keyName.color = color;

        public void SetGamepadKeyIconContainerActive(bool isActive) => _gamepadKeyIconContainer.gameObject.SetActive(isActive);

        public void SetGamepadKeyIcon(Sprite icon) => _gamepadKeyIcon.sprite = icon;
    }
}