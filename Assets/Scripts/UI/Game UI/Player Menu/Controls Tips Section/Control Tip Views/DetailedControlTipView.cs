using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.PlayerMenu;
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
        if (_keyIconContainer.isActiveAndEnabled)
        {
            _keyIconContainer.color = newColor;
        }
        if (_keyName.isActiveAndEnabled)
        {
            _keyName.color = newColor;
        }
    }
}
