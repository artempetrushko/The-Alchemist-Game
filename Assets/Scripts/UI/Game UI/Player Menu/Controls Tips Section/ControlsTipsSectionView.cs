using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsTipsSectionView : MonoBehaviour
{
    [SerializeField]
    private DetailedControlTipView controlTipViewPrefab;

    public void SetContent(DetailedControlTip[] actionTips)
    {
        ClearContent();
        foreach (var tip in actionTips)
        {
            var actionTipView = Instantiate(controlTipViewPrefab, transform);
            actionTipView.SetInfo(tip);
        }
    }

    public void ClearContent()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
