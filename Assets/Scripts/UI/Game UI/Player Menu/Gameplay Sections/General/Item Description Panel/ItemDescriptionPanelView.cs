using TMPro;
using UnityEngine;

namespace UI.PlayerMenu
{
    public class ItemDescriptionPanelView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _itemTitleText;
        [SerializeField] private TMP_Text _itemDescriptionText;
        [SerializeField] private GameObject _parametersContainer;

        public GameObject ParametersContainer => _parametersContainer;

        public Rect Rect => GetComponent<RectTransform>().rect;

        public void Show() => _canvasGroup.alpha = 1f;

        public void Hide() => _canvasGroup.alpha = 0f;

        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetItemTitleText(string text) => _itemTitleText.text = text;

        public void SetItemDescriptionText(string text) => _itemDescriptionText.text = text;
    }
}