using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class ItemsInteractionButton : MonoBehaviour
    {
        [SerializeField] private Button _buttonComponent;
        [SerializeField] private Image _interactionIcon;
        [SerializeField] private TMP_Text _interactionTitle;

        public Button ButtonComponent => _buttonComponent;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetInteractionIcon(Sprite icon) => _interactionIcon.sprite = icon;

        public void SetInteractionTitleText(string text) => _interactionTitle.text = text;
    }
}