using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredComponentPartState
{
    private ComponentPartType componentPartType;
    private ComponentPartState componentPartState;

    public ComponentPartType ComponentPartType => componentPartType;
    public ComponentPartState ComponentPartState
    {
        get => componentPartState;
        set
        {
            if (value.ComponentPartData.Type == componentPartType)
            {
                componentPartState = value;
            }
        }
    }

    public RequiredComponentPartState(RequiredComponentPart componentPart)
    {
        componentPartType = componentPart.ComponentPartType;
        if (componentPart.ComponentPart != null)
        {
            componentPartState = new ComponentPartState(componentPart.ComponentPart);
        }        
    }
}
