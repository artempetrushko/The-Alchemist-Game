using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public event Action ObjectDestroyed; 

    [SerializeField]
    protected string title;
    [SerializeField]
    protected string interactionDescription;

    public string Title => title;
    public string InteractionDescription => interactionDescription;

    private void OnDestroy()
    {
        ObjectDestroyed?.Invoke();
    }
}
