using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.EnvironmentExploration
{
    public class PortalStabilizationPanelView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _progressBar;
        [SerializeField] private Image _progressBarFillingArea;

        public CanvasGroup CanvasGroup => _canvasGroup;
        public Rect ProgressBarRect => _progressBar.rectTransform.rect;
        public Rect ProgressBarFilingAreaRect => _progressBarFillingArea.rectTransform.rect;

        public void SetProgressBarFillingAreaActive(bool isActive) => _progressBarFillingArea.gameObject.SetActive(isActive);

        public void SetProgressBarFillingAreaSizeDelta(Vector2 sizeDelta) => _progressBarFillingArea.rectTransform.sizeDelta = sizeDelta;
    }
}