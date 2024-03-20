using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderOptionView : SettingsOptionView
{
    [SerializeField]
    private Slider slider;

    public override void SetValueChangedAction(Action<int> optionValueChangedAction)
    {
        slider.onValueChanged.AddListener((value) => optionValueChangedAction((int)value));
    }

    public void SetDefaultSliderValues(int minValue, int maxValue, int currentValue)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }
}
