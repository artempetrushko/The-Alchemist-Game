using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private HUDView hudView;

    public IEnumerator ShowLocationName_COR(string locationName)
    {
        yield return StartCoroutine(hudView.ShowLocationName_COR(locationName));
    }

    public IEnumerator HideStartBlackScreen_COR()
    {
        yield return StartCoroutine(hudView.HideStartBlackScreen_COR());
    }
}
