using TMPro;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class ItemDescriptionPanelView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _itemTitleText;
        [SerializeField] private TMP_Text _itemDescriptionText;
        [Space]
        [SerializeField] private ItemParameterView _parameterViewPrefab;
        [SerializeField] private GameObject _parametersContainer;

        public Rect Rect => GetComponent<RectTransform>().rect;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetVisible(bool isVisible) => _canvasGroup.alpha = isVisible ? 1f : 0f;

        public ItemParameterView GetOrCreateParameterViewByIndex(int index)
        {
            if (index >= _parametersContainer.transform.childCount)
            {
                while (index >= _parametersContainer.transform.childCount)
                {
                    Instantiate(_parameterViewPrefab, _parametersContainer.transform);
                }
            }
            return _parametersContainer.transform.GetChild(index).GetComponent<ItemParameterView>();
        }

        public void DisableAllParameterViews()
        {
            foreach (var parameterView in _parametersContainer.GetComponentsInChildren<ItemParameterView>())
            {
                parameterView.SetActive(false);
            }
        }

        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetItemTitleText(string text) => _itemTitleText.text = text;

        public void SetItemDescriptionText(string text) => _itemDescriptionText.text = text;
    }
}