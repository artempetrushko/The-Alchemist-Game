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
    [Space, SerializeField]
    private PlayerItemPicker playerItemPicker;
    [SerializeField]
    private PlayerInteractor playerInteractor;
    [SerializeField]
    private ItemsContainersManager itemsContainersManager;
    [Space, SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private InputManager inputManager;

    private InteractiveObjectInfoView currentInteractiveObjectInfoView;
    private InteractiveObject currentInteractiveObject;

    public void PickItems(InputAction.CallbackContext context)
    {
        if (context.performed && playerItemPicker != null)
        {
            var pickableItems = playerItemPicker.PickableItems;
            if (pickableItems.Count > 0)
            {
                foreach (var item in pickableItems)
                {
                    if (inventoryManager.AddNewItemState(item.CurrentItemState))
                    {
                        Destroy(item.gameObject);
                    }
                }
                pickableItems.Clear();
            }
        }      
    }

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
                    InteractDungeonPortal(dungeonPortal);
                    break;
            }
        }
    }

    private IEnumerator StabilizePortal_COR()
    {
        yield return null;
        //StartCoroutine(portalStabilizationProgressSection.FillProgressBar_COR(stabilizationTimeInSeconds));
    }

    

    private void InteractDungeonPortal(DungeonPortal portal)
    {
        switch (portal.PortalState)
        {
            case PortalState.Unstable:
                /*if (gameManager.StabilizerCreatedAndAvailable)
                {
                    inventoryManager.RemoveItem("Стабилизатор портала");
                    GetComponent<Collider>().enabled = false;
                    //FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                    StartCoroutine(StabilizePortal_COR());
                    //PortalState = PortalState.Stabilizing;
                }
                else
                {
                    GetComponent<Collider>().enabled = false;
                    //FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                    //levelFinisher.Activate();
                }
                break;*/
                break;

            case PortalState.Stable:
                GetComponent<Collider>().enabled = false;
                //FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                //levelFinisher.FinishGame();
                break;
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

    //private void DisableI

    private void ShowCurrentInteractiveObjectControlsTips()
    {
        if (currentInteractiveObject != null)
        {
            var actionName = currentInteractiveObject switch
            {
                ItemsContainer => "Открыть",
                DungeonPortal => "Использовать"
            };
            inputManager.ShowCurrentControlsTips(currentInteractiveObjectInfoView.ControlsTipsSectionView, new[]
            {
                (actionName, inputManager.PlayerActions.Player.Interact)
            });
        }      
    }
}
