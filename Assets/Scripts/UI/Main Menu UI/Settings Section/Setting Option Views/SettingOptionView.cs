using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

public abstract class SettingsOptionView : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text optionTitleText;
    [SerializeField]
    protected TMP_Text optionValueText;

    public abstract void SetValueChangedAction(Action<int> optionValueChangedAction);

    public void SetTitleReference((TableReference table, TableEntryReference entry) localizedString)
    {
        optionTitleText.GetComponent<LocalizeStringEvent>().StringReference.SetReference(localizedString.table, localizedString.entry);
    }

    public void SetOptionValue(string optionValue) => optionValueText.text = optionValue;
}
