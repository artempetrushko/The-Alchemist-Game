using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class MainMenuButtonData
{
    [SerializeField]
    private string labelText;
    [SerializeField]
    private UnityEvent onButtonPressed;

    public string LabelText => labelText;
    public UnityEvent OnButtonPressed => onButtonPressed;
}
