using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemistHubPortal : InteractiveObject
{
    public event Action PlayerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerState>() != null)
        {
            PlayerDetected?.Invoke();
        }
    }
}
