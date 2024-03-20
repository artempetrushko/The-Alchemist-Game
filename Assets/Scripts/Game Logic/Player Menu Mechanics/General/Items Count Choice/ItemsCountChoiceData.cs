using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemsCountChoiceAction
{
    public readonly string Description;
    public readonly UnityAction Action;

    public ItemsCountChoiceAction(string description, UnityAction action)
    {
        Description = description;
        Action = action;
    }
}

public class ItemsCountChoiceData
{
    public ItemsCountChoiceAction ConfirmAction;
    public ItemsCountChoiceAction ConfirmAllAction;
    public ItemsCountChoiceAction CancelAction;
}
