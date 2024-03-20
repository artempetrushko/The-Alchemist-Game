using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningWindow : MonoBehaviour
{
    [SerializeField]
    private TMP_Text messageText;
    [SerializeField]
    private GameObject actionButtonsContainer;
    [SerializeField]
    private ActionButton actionButtonPrefab;

    public void EnableAndSetInfo(string message, List<Tuple<string, Action>> actionInfos)
    {

    }
}
