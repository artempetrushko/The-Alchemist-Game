using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetailedControlTipView : ControlTipView
{
    [Space, SerializeField]
    private TMP_Text actionTitle;

    public void SetInfo(DetailedControlTip actionTip)
    {
        base.SetInfo(actionTip);
        actionTitle.text = actionTip.ActionTitle;
    }

    public void ChangeContentColor(Color newColor)
    {
        actionTitle.color = newColor;
        if (keyIconContainer.isActiveAndEnabled)
        {
            keyIconContainer.color = newColor;
        }
        if (keyName.isActiveAndEnabled)
        {
            keyName.color = newColor;
        }
    }
}
