using UnityEngine;

public class DetailedControlTip : ControlTip
{
    public readonly string ActionTitle;

    public DetailedControlTip(string actionTitle, string keyName) : base(keyName)
    {
        ActionTitle = actionTitle;
    }

    public DetailedControlTip(string actionTitle, Sprite keyIcon) : base(keyIcon)
    {
        ActionTitle = actionTitle;
    }
}