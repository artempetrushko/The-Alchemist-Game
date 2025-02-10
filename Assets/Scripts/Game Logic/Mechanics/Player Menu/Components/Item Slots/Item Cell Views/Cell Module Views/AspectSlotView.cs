using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectSlotView : MonoBehaviour
{
    [SerializeField]
    private Image border;
    [SerializeField]
    private Image innerArea;

    public void SetAppearance(Sprite innerAreaIcon, float innerAreaFillAmount)
    {
        innerArea.sprite = innerAreaIcon;
        innerArea.fillAmount = innerAreaFillAmount;
    }
}
