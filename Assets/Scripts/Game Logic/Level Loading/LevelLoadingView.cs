using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.LevelLoading
{
    public class LevelLoadingView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loadingInfoSectionCanvasGroup;
        [SerializeField] private Rect _progressBarFillAreaContainerRect;
        [SerializeField] private Image _progressBarFillArea;
        [SerializeField] private GameObject _continueTipSection;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetProgressBarFillAmount(float loadingProgress)
        {
            _progressBarFillArea.rectTransform.sizeDelta = new Vector2(_progressBarFillAreaContainerRect.width * (loadingProgress + 0.1f), _progressBarFillArea.rectTransform.rect.height);
        }

        public void SetContinueTipSectionActive(bool isActive) => _continueTipSection.SetActive(isActive);

        public void SetLoadingInfoSectionAlpha(float alpha) => _loadingInfoSectionCanvasGroup.alpha = alpha;
    }
}