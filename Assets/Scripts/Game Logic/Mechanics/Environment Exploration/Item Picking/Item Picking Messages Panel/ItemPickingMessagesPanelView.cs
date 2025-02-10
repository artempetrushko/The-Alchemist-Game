using System.Linq;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
	public class ItemPickingMessagesPanelView : MonoBehaviour
	{
        [SerializeField] private GameObject _itemPickingMessageViewsContainer;
        [SerializeField] private ItemPickingMessageView _itemPickingMessageViewPrefab;

        public ItemPickingMessageView CreateItemPickingMessageView() => Instantiate(_itemPickingMessageViewPrefab, _itemPickingMessageViewsContainer.transform);

        public ItemPickingMessageView GetItemPickingMessageViewByIndex(int index) => _itemPickingMessageViewsContainer.transform.GetChild(index).GetComponent<ItemPickingMessageView>();

        public ItemPickingMessageView GetInactiveItemPickingMessageView()
        {
            return _itemPickingMessageViewsContainer
                .GetComponentsInChildren<ItemPickingMessageView>()
                .FirstOrDefault(view => view.isActiveAndEnabled);
        }

        public void DisableAllItemPickingMessageViews()
        {
            foreach (var itemPickingMessageView in _itemPickingMessageViewsContainer.GetComponentsInChildren<ItemPickingMessageView>())
            {
                itemPickingMessageView.SetActive(false);
            }
        }
    }
}