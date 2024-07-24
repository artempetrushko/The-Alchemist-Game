using UnityEngine;

public abstract class InteractiveObjectHandler : MonoBehaviour
{ }

public abstract class InteractiveObjectHandler<T> : InteractiveObjectHandler where T : InteractiveObject
{
    public abstract void HandleInteraction(T interactiveObject);
}
