using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
	public class AspectView : MonoBehaviour
	{
		[SerializeField] private Image _border;
		[SerializeField] private Image _innerArea;

		public void SetActive(bool isActive) => gameObject.SetActive(isActive);

		public void SetInnerAreaImage(Sprite image) => _innerArea.sprite = image;

		public void SetInnerAreaFillAmount(float fillAmount) => _innerArea.fillAmount = fillAmount;
	}
}