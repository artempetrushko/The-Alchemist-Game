using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemParameterView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text parameterTitleText;
    [SerializeField]
    private TMP_Text parameterValueText;

    public void SetInfo(string parameterTitle, string parameterValue)
    {
        parameterTitleText.text = parameterTitle;
        parameterValueText.text = parameterValue;
    }
}
