using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class PlayerMenuSectionSelectButton : MonoBehaviour
    {
        [SerializeField] private Button _defaultStateButton;
        [SerializeField] private Image _defaultStateButtonIcon;
        [SerializeField] private Image _selectedStateButton;
        [SerializeField] private Image _selectedStateButtonIcon;

        public Button ButtonComponent => _defaultStateButton;

        public void SetInteractable(bool isInteractable)
        {
            _defaultStateButton.gameObject.SetActive(!isInteractable);
            _selectedStateButton.gameObject.SetActive(isInteractable);
        }

        public void SetSectionIcon(Sprite sectionIcon)
        {
            _defaultStateButtonIcon.sprite = _selectedStateButtonIcon.sprite = sectionIcon;
        }
    }
}