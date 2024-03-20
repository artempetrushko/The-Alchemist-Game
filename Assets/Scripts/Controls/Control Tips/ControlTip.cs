using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlTip
{
    public string KeyName { get; private set; }
    public Sprite KeyIcon { get; private set; }

    public ControlTip(string keyName) 
    {
        KeyName = keyName;
    }

    public ControlTip(Sprite keyIcon)
    {
        KeyIcon = keyIcon;
    }
}
