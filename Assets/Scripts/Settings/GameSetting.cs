using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameSetting { }

public class GameSetting<T> : GameSetting
{
    private string title;
    private (T value, string formattedValue)[] settingValues;
    private Action<T> applyValueAction;
    private SettingView settingView;
    private int currentValueIndex;

    private int CurrentValueIndex
    {
        get => currentValueIndex;
        set
        {
            currentValueIndex = value;
            if (currentValueIndex >= settingValues.Length)
            {
                currentValueIndex = 0;
            }
            else if (currentValueIndex < 0)
            {
                currentValueIndex = settingValues.Length - 1;
            }
        }
    }

    public GameSetting(string title, SettingView settingView, (T value, string formattedValue)[] formattedValues, Action<T> applyValueAction)
    {
        this.title = title;
        this.settingView = settingView;
        settingValues = formattedValues;
        this.applyValueAction = applyValueAction;
    }

    public void ChangeValue(int offset) => CurrentValueIndex += offset;

    public void ApplyValue()
    {
        applyValueAction.Invoke(settingValues[CurrentValueIndex].value);
    }
}
