using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToggleActionDescription
{
    EnableObjects,
    DisableObjects
}

public enum ToggleCondition
{
    PassageEnabled,
    PassageDisabled,
}

[Serializable]
public class ToggleEnvironmentObjectsInfo
{
    [SerializeField]
    private List<GameObject> environmentObjects = new List<GameObject>();
    [Space]
    [SerializeField]
    [Tooltip("Какое действие необходимо производить над объектами")]
    private ToggleActionDescription toggleAction;
    [SerializeField]
    [Tooltip("Условие, при котором будет производиться действие над объектами")]
    private ToggleCondition toggleCondition;

    public ToggleCondition ToggleCondition => toggleCondition;

    public void ExecuteToggleAction(ToggleCondition currentCondition)
    {
        if (currentCondition == toggleCondition)
        {
            foreach (var obj in environmentObjects)
            {
                obj.SetActive(toggleAction == ToggleActionDescription.EnableObjects);
            }
        }        
    }
}
