using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableObjectHandler
{
    void HandleInteraction<T>(T interactiveObject) where T : InteractiveObject;
}
