using LeTai.TrueShadow;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationTitleView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text locationTitleText;
    [SerializeField]
    private TrueShadow titleShadow;
    [Space, SerializeField]
    private float titleAppearanceTime = 1f;

    public IEnumerator ShowLocationName_COR(string locationTitle)
    {
        locationTitleText.text = "";
        titleShadow.enabled = true;
        var textAppearanceLatency = titleAppearanceTime / locationTitle.Length;
        for (var i = 0; i < locationTitle.Length; i++)
        {
            locationTitleText.text += locationTitle[i];
            yield return new WaitForSecondsRealtime(textAppearanceLatency);
        }
        yield return new WaitForSecondsRealtime(5f);
        for (var i = 0; i < locationTitle.Length; i++)
        {
            locationTitleText.text = locationTitleText.text[..^1];
            yield return new WaitForSecondsRealtime(textAppearanceLatency / 2);
        }
        titleShadow.enabled = false;
    }
}
