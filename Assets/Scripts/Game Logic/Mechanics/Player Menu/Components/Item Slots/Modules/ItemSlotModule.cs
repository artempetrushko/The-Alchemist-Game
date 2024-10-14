using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
	public abstract class ItemSlotModule : MonoBehaviour
	{
		[SerializeField] protected CanvasGroup _canvasGroup;

		public abstract void UpdateDisplayedInfo(ItemState item);

		public void SetActive(bool isActive) => gameObject.SetActive(isActive);

		public void SetVisible(bool isVisible) => _canvasGroup.alpha = isVisible ? 1 : 0;
	}
}