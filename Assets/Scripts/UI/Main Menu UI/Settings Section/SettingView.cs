using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text settingTitleText;
    [SerializeField]
    private TMP_Text settingValueText;
    [SerializeField]
    private Button leftChooseSettingValueButton;
    [SerializeField]
    private Button rightChooseSettingValueButton;
    [SerializeField]
    private Image background;

   /* private int CurrentSettingValueIndex
    {
        get => settingData.CurrentSettingValueIndex;
        set
        {
            if (value >= 0 && value < settingData.SettingValuesCount)
            {
                settingValueText.text = settingData.ChangeSettingValue(value);
                ToggleChooseSettingValueButtonState(leftChooseSettingValueButton, value > 0);
                ToggleChooseSettingValueButtonState(rightChooseSettingValueButton, value < settingData.SettingValuesCount - 1);
            }
        }
    }

    public void ToggleBackgroundState(bool isEnable) => background.gameObject.SetActive(isEnable);

    public void ShiftNeighboringSettingValue(int offset) => CurrentSettingValueIndex += offset;

    public void SetData(SettingData settingData)
    {
        this.settingData = settingData;
        settingTitleText.text = settingData.Title;
        CurrentSettingValueIndex = settingData.CurrentSettingValueIndex;
    }

    private void ToggleChooseSettingValueButtonState(Button button, bool isEnable)
    {
        button.enabled = isEnable;
        button.GetComponent<Image>().enabled = isEnable;
    }*/
}
