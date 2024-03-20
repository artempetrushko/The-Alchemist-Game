using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchesOptionView : SettingsOptionView
{
    [SerializeField]
    private Button previousValueButton;
    [SerializeField]
    private Button nextValueButton;

    public override void SetValueChangedAction(Action<int> optionValueChangedAction)
    {
        previousValueButton.onClick.AddListener(() => optionValueChangedAction(-1));
        nextValueButton.onClick.AddListener(() => optionValueChangedAction(1));
    }
}
