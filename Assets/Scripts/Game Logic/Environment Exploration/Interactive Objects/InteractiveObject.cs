using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public event Action ObjectDestroyed; 

    [SerializeField]
    protected string title;

    public string Title => title;

    private void OnDestroy()
    {
        ObjectDestroyed?.Invoke();
    }
}
