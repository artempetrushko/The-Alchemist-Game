using Controls;
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

    private InputManager _inputManager;

    [Space, SerializeField]
    private ItemsContainersManager itemsContainersManager;
    [SerializeField]
    private PortalsManager portalsManager; 

    private InteractiveObjectInfoView currentInteractiveObjectInfoView;
    private InteractiveObject currentInteractiveObject;

    public EnvironmentInteractionsManager(InputManager inputManager)
    {
        _inputManager = inputManager;
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
                    portalsManager.InteractDungeonPortal(dungeonPortal);
                    break;
            }
        }
    }

    private void OnEnable()
    {
        playerInteractor.InteractiveObjectDetected += SetCurrentInteractiveObject;
        playerInteractor.InteractiveObjectLost += DeleteCurrentInteractiveObject;
        _inputManager.SubscribeControlsChangedEvent(ShowCurrentInteractiveObjectControlsTips);
    }

    private void OnDisable()
    {
        playerInteractor.InteractiveObjectDetected -= SetCurrentInteractiveObject;
        playerInteractor.InteractiveObjectLost -= DeleteCurrentInteractiveObject;
        _inputManager.UnsubscribeControlsChangedEvent(ShowCurrentInteractiveObjectControlsTips);
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
