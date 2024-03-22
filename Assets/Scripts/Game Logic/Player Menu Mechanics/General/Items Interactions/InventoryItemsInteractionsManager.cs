using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InventoryItemsInteractionsManager : MonoBehaviour
{
    [SerializeField]
    private ItemsInteractionModule[] interactionModules;
    [SerializeField]
    private ItemCellActionsView itemCellActionsViewPrefab;
    [SerializeField]
    private GameObject interactionProcessBackgroundPrefab;
    [SerializeField]
    private GameObject interactionProcessBackgroundContainer;
    [Space, SerializeField]
    private ItemsCountChoiceManager itemsCountChoiceManager;
    [Space, SerializeField]
    private InputManager inputManager;

    private ItemCellActionsView itemCellActionsView;
    private GameObject interactionProcessBackground;
    private int currentActionButtonNumber;
    private ItemsInteractionModule currentItemsInteraction;
    private PlayerInputActionMap previousActionMap;

    private int CurrentActionButtonNumber
    {
        get => currentActionButtonNumber;
        set
        {
            if (value > 0 && value <= itemCellActionsView.ActionButtonsCount)
            {
                currentActionButtonNumber = value;
                itemCellActionsView.SelectActionButton(currentActionButtonNumber);
            }
        }
    }

    public ItemsInteractionModule CurrentItemsInteraction
    {
        get => currentItemsInteraction;
        private set
        {
            if (currentItemsInteraction != value)
            {
                currentItemsInteraction = value;
            }
        }
    }

    public void CreateItemSlotActionsMenu(ItemSlot itemSlot)
    {
        interactionProcessBackground = Instantiate(interactionProcessBackgroundPrefab, interactionProcessBackgroundContainer.transform);
        itemCellActionsView = Instantiate(itemCellActionsViewPrefab, interactionProcessBackgroundContainer.transform);
        itemCellActionsView.SetPosition(itemSlot.CellView);
        itemCellActionsView.CreateActionButtons(GetItemSlotActionDatas(itemSlot));
        CurrentActionButtonNumber = 1;
        previousActionMap = inputManager.CurrentActionMap;
        inputManager.CurrentActionMap = PlayerInputActionMap.PlayerMenu_ItemCellActionsMenu;
    }

    public void NavigateActionsMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var inputValue = context.ReadValue<Vector2>();
            if (Mathf.Abs(inputValue.y) == 1)
            {
                CurrentActionButtonNumber -= (int)inputValue.y;
            }
        }
    }

    public void PressSelectedButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            itemCellActionsView.ClickActionButton(CurrentActionButtonNumber);
        }
    }

    public void QuitActionsMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ClearActionsMenu();
            inputManager.CurrentActionMap = previousActionMap;
        }
    }

    public void ExecuteInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && currentItemsInteraction is IInteractionExecutable executableInteraction)
        {
            executableInteraction.Execute();
        }
    }

    public void CancelInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && currentItemsInteraction is IInteractionCancelable cancelableInteraction)
        {
            cancelableInteraction.CancelInteraction();
        }
    }

    private (string actionDescription, Sprite icon, UnityAction action)[] GetItemSlotActionDatas(ItemSlot itemSlot)
    {
        var actionDatas = new List<(string actionDescription, Sprite icon, UnityAction action)>();
        foreach (var itemInteraction in itemSlot.GetItemInteractions())
        {
            var accordingInteractionModule = interactionModules.FirstOrDefault(interactionData => interactionData.Interaction == itemInteraction);
            if (accordingInteractionModule != null)
            {
                actionDatas.Add((accordingInteractionModule.DisplayedName, accordingInteractionModule.Icon, () =>
                {
                    CurrentItemsInteraction = accordingInteractionModule;
                    CurrentItemsInteraction.StartInteraction(itemSlot);
                    ClearActionsMenu();
                }));
            }
        }
        return actionDatas.ToArray();
    }

    private void ClearActionsMenu()
    {
        Destroy(itemCellActionsView.gameObject);
        Destroy(interactionProcessBackground);
    }
}
