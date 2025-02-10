using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ItemsCountChoiceManager : MonoBehaviour
{
    [SerializeField]
    private ChooseItemsCountView itemsCountChoiceViewPrefab;
    [SerializeField]
    private GameObject itemsCountChoiceViewContainer;
    [Space, SerializeField]
    private InputManager inputManager;

    private ChooseItemsCountView itemsCountChoiceView;
    private ItemsCountChoiceData itemsCountChoiceData;
    private int selectedItemsCount;

    public int SelectedItemsCount
    {
        get => selectedItemsCount;
        private set
        {
            selectedItemsCount = value;
            itemsCountChoiceView.SetItemsCounterText(selectedItemsCount);
        }
    }

    public void CreateItemsCountChoiceView(string interactionDescription, StackableItemState stackableItem, int minItemsCount, int maxItemsCount, ItemsCountChoiceData possibleActions)
    {
        itemsCountChoiceView = Instantiate(itemsCountChoiceViewPrefab, itemsCountChoiceViewContainer.transform);
        itemsCountChoiceView.StartItemsCountChoosing(stackableItem.BaseParams.Icon, minItemsCount, maxItemsCount, interactionDescription, GetActionButtonsParams(possibleActions));
        itemsCountChoiceView.SetSliderValueChangedAction((value) => SelectedItemsCount = (int)value);
        inputManager.CurrentActionMap = PlayerInputActionMap.PlayerMenu_ItemsCountChoiceView;
    }

    public void ClearItemsCountChoiceView() => Destroy(itemsCountChoiceView.gameObject);

    public void InvokeConfirmAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            itemsCountChoiceData.ConfirmAction.Action.Invoke();
        }
    }

    public void InvokeConfirmAllAction(InputAction.CallbackContext context)
    {
        if (context.performed && itemsCountChoiceData.ConfirmAllAction != null)
        {
            itemsCountChoiceData.ConfirmAllAction.Action.Invoke();
        }
    }

    public void InvokeCancelAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            itemsCountChoiceData.CancelAction.Action.Invoke();
        }
    }

    public void ChangeItemsCounterValue(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>().x;
        if (Mathf.Abs(inputValue) == 1)
        {
            SelectedItemsCount += (int)inputValue;
        }
    }

    public List<(DetailedControlTip controlTip, UnityAction buttonAction)> GetActionButtonsParams(ItemsCountChoiceData possibleActions)
    {
        var itemsCountChoiceInputActions = inputManager.PlayerActions.PlayerMenuChooseItemsCountView;
        var actionButtonParams = new List<(string name, InputAction inputAction, UnityAction buttonPressedAction)>()
        {
            (possibleActions.ConfirmAction.Description, itemsCountChoiceInputActions.Confirm, possibleActions.ConfirmAction.Action),
            (possibleActions.CancelAction.Description, itemsCountChoiceInputActions.Cancel, possibleActions.CancelAction.Action)
        };
        if (possibleActions.ConfirmAllAction != null)
        {
            actionButtonParams.Insert(1, (possibleActions.ConfirmAllAction.Description, itemsCountChoiceInputActions.ConfirmAll, possibleActions.ConfirmAllAction.Action));
        }
        return actionButtonParams
            .Select(actionButtonData => (inputManager.CreateDetailedControlsTip((actionButtonData.name, actionButtonData.inputAction)), actionButtonData.buttonPressedAction))
            .ToList();
    }
}
