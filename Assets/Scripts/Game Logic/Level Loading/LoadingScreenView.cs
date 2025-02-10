using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.LevelLoading
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField]
        private GameObject loadingInfoSection;
        [SerializeField]
        private Image progressBarFillAreaContainer;
        [SerializeField]
        private Image progressBarFillArea;
        [SerializeField]
        private GameObject continueButtonSection;

        private Rect progressBarFillAreaContainerRect;

        public void SetProgressBarFillAmount(float loadingProgress)
        {
            progressBarFillArea.rectTransform.sizeDelta = new Vector2(progressBarFillAreaContainerRect.width * (loadingProgress + 0.1f), progressBarFillArea.rectTransform.rect.height);
        }

        public void ShowContinueButton()
        {
            loadingInfoSection.GetComponent<CanvasGroup>().alpha = 0f;
            continueButtonSection.SetActive(true);
        }

        private void OnEnable()
        {
            progressBarFillAreaContainerRect = progressBarFillAreaContainer.rectTransform.rect;
        }
    }
}