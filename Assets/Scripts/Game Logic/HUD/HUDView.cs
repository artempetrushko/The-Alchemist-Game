using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    [SerializeField]
    private LocationTitleView locationTitleView;
    [SerializeField]
    private Image startBlackScreen;

    public IEnumerator ShowLocationName_COR(string locationName)
    {
        yield return StartCoroutine(locationTitleView.ShowLocationName_COR(locationName));
    }

    public IEnumerator HideStartBlackScreen_COR()
    {
        startBlackScreen.gameObject.SetActive(true);
        var hideBlackScreenTween = startBlackScreen.DOFade(0f, 2f);
        yield return hideBlackScreenTween.WaitForCompletion();
    }
}
