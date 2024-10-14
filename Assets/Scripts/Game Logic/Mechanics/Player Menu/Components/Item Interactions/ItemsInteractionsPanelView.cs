using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
	public class ItemsInteractionsPanelView : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private ItemsInteractionButton _interactionButtonPrefab;
        [SerializeField] private GameObject _interactionButtonsContainer;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public ItemsInteractionButton GetOrCreateInteractionButtonByIndex(int index)
        {
            if (index >= _interactionButtonsContainer.transform.childCount)
            {
                while (index >= _interactionButtonsContainer.transform.childCount)
                {
                    Instantiate(_interactionButtonPrefab, _interactionButtonsContainer.transform);
                }
            }
            return _interactionButtonsContainer.transform.GetChild(index).GetComponent<ItemsInteractionButton>();
        }

        public void DisableAllInteractionButtons()
        {
            foreach (var interactionButton in _interactionButtonsContainer.GetComponentsInChildren<ItemsInteractionButton>())
            {
                interactionButton.SetActive(false);
            }
        }
    }
}