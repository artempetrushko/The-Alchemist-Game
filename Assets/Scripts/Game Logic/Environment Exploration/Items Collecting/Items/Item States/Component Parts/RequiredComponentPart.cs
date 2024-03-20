using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RequiredComponentPart
{
    [SerializeField]
    private ComponentPartType componentPartType;
    [SerializeField]
    private ComponentPartData componentPart;

    public ComponentPartType ComponentPartType => componentPartType;
    public ComponentPartData ComponentPart => componentPart;
}
