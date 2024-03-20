using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedControlTip : ControlTip
{
    public string ActionTitle { get; private set; }

    public DetailedControlTip(string actionTitle, string keyName) : base(keyName)
    {
        ActionTitle = actionTitle;
    }

    public DetailedControlTip(string actionTitle, Sprite keyIcon) : base(keyIcon)
    {
        ActionTitle = actionTitle;
    }
}