using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public event Action<InteractiveObject> InteractiveObjectDetected;
    public event Action InteractiveObjectLost;

    private InteractiveObject currentInteractiveObject;

    private InteractiveObject CurrentInteractiveObject
    {
        get => currentInteractiveObject;
        set
        {
            if (currentInteractiveObject != value)
            {
                if (currentInteractiveObject != null)
                {
                    InteractiveObjectLost?.Invoke();
                    currentInteractiveObject.ObjectDestroyed -= ClearCurrentInteractiveObject;
                }
                currentInteractiveObject = value;
                if (currentInteractiveObject != null)
                {
                    InteractiveObjectDetected?.Invoke(currentInteractiveObject);
                    currentInteractiveObject.ObjectDestroyed += ClearCurrentInteractiveObject;
                }
            }         
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var potentialInteractiveObject = other.GetComponent<InteractiveObject>();
        if (potentialInteractiveObject != null && CurrentInteractiveObject == null)
        {
            CurrentInteractiveObject = potentialInteractiveObject;
        }
    }

    private void OnTriggerExit(Collider other) => ClearCurrentInteractiveObject();

    private void ClearCurrentInteractiveObject() => CurrentInteractiveObject = null;
}
