using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnvironmentInteractionsManager : MonoBehaviour
{
    [SerializeField]
    private InteractiveObjectInfoView interactiveObjectInfoViewPrefab;
    [SerializeField]
    private GameObject interactiveObjectInfoViewContainer;    
    [SerializeField]
    private PlayerInteractor playerInteractor;
    [SerializeField]
    private InputManager inputManager;
    [Space, SerializeField]
    private ItemsContainersManager itemsContainersManager;
    [SerializeField]
    private PortalsManager portalsManager; 

    private InteractiveObjectInfoView currentInteractiveObjectInfoView;
    private InteractiveObject currentInteractiveObject;

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
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
    }

    private void OnEnable()
    {
        playerInteractor.InteractiveObjectDetected += SetCurrentInteractiveObject;
        playerInteractor.InteractiveObjectLost += DeleteCurrentInteractiveObject;
        inputManager.SubscribeControlsChangedEvent(ShowCurrentInteractiveObjectControlsTips);
    }

    private void OnDisable()
    {
        playerInteractor.InteractiveObjectDetected -= SetCurrentInteractiveObject;
        playerInteractor.InteractiveObjectLost -= DeleteCurrentInteractiveObject;
        inputManager.UnsubscribeControlsChangedEvent(ShowCurrentInteractiveObjectControlsTips);
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
            inputManager.ShowCurrentControlsTips(currentInteractiveObjectInfoView.ControlsTipsSectionView, new[]
            {
                (currentInteractiveObject.InteractionDescription, inputManager.PlayerActions.Player.Interact)
            });
        }      
    }
}
