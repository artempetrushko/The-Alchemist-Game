using Controls;
using GameLogic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnvironmentInteractionsManager : IDisposable
{
    private InteractiveObjectInfoView interactiveObjectInfoViewPrefab;
    private GameObject interactiveObjectInfoViewContainer;    
    private PlayerInteractor playerInteractor;

    private InputManager _inputManager;

    private ItemsContainersManager itemsContainersManager;
    private PortalsManager portalsManager; 

    private InteractiveObjectInfoView currentInteractiveObjectInfoView;
    private InteractiveObject currentInteractiveObject;

    public EnvironmentInteractionsManager(InputManager inputManager)
    {
        _inputManager = inputManager;

        _inputManager.PlayerActions.Player.Interact.performed += Interact;

        playerInteractor.InteractiveObjectDetected += SetCurrentInteractiveObject;
        playerInteractor.InteractiveObjectLost += DeleteCurrentInteractiveObject;
        _inputManager.SubscribeControlsChangedEvent(ShowCurrentInteractiveObjectControlsTips);
    }

    public void Dispose()
    {
        _inputManager.PlayerActions.Player.Interact.performed -= Interact;

        playerInteractor.InteractiveObjectDetected -= SetCurrentInteractiveObject;
        playerInteractor.InteractiveObjectLost -= DeleteCurrentInteractiveObject;
        _inputManager.UnsubscribeControlsChangedEvent(ShowCurrentInteractiveObjectControlsTips);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        switch (currentInteractiveObject)
        {
            case ItemsContainer itemsContainer:
                itemsContainersManager.OpenContainer(itemsContainer);
                break;

            case DungeonPortal dungeonPortal:
                portalsManager.InteractDungeonPortal(dungeonPortal);
                break;
        }
    }

    private void SetCurrentInteractiveObject(InteractiveObject interactiveObject)
    {
        if (currentInteractiveObject != interactiveObject)
        {
            currentInteractiveObject = interactiveObject;
            currentInteractiveObjectInfoView = Instantiate(interactiveObjectInfoViewPrefab, interactiveObjectInfoViewContainer.transform);
            currentInteractiveObjectInfoView.SetInfo(currentInteractiveObject.Title, currentInteractiveObject.transform);
            ShowCurrentInteractiveObjectControlsTips();
        }      
    }

    private void DeleteCurrentInteractiveObject()
    {
        currentInteractiveObject = null;
        Destroy(currentInteractiveObjectInfoView.gameObject);
    }

    private void ShowCurrentInteractiveObjectControlsTips()
    {
        if (currentInteractiveObject != null)
        {
            _inputManager.ShowCurrentControlsTips(currentInteractiveObjectInfoView.ControlsTipsSectionView, new[]
            {
                (currentInteractiveObject.InteractionDescription, _inputManager.PlayerActions.Player.Interact)
            });
        }      
    }
}
