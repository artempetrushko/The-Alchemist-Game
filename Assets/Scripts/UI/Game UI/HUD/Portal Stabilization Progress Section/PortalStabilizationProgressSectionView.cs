using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalStabilizationProgressSectionView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup viewCanvasGroup;
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private Image progressBarFillingArea;

    public IEnumerator FillProgressBar_COR(int fillingTimeInSeconds)
    {
        yield return StartCoroutine(ChangeViewVisibility_COR(true));
        progressBarFillingArea.gameObject.SetActive(true);
        for (var i = 1; i <= fillingTimeInSeconds; i++)
        {
            progressBarFillingArea.rectTransform.sizeDelta = new Vector2(progressBar.rectTransform.rect.width * (0.1f + i / fillingTimeInSeconds), progressBarFillingArea.rectTransform.rect.height);
            yield return new WaitForSecondsRealtime(1f);
        }
        yield return StartCoroutine(ChangeViewVisibility_COR(false));
        progressBarFillingArea.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        progressBarFillingArea.gameObject.SetActive(false);
    }

    private IEnumerator ChangeViewVisibility_COR(bool isVisible)
    {
        var visibilityChangeTween = viewCanvasGroup.DOFade(isVisible ? 1f : 0f, 1f);
        yield return visibilityChangeTween.WaitForCompletion();
    }
}
