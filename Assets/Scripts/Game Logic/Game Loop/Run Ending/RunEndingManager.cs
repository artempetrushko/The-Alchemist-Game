using Controls;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RunEndingManager : IDisposable
{
    [SerializeField]
    private RunEndingStatusData[] runEndingStatusDatas;
    [Space, SerializeField]
    private RunEndingView runEndingViewPrefab;
    [SerializeField]
    private GameObject runEndingViewContainer;

    private InputManager _inputManager;

    private LevelLoadingManager levelLoadingManager;

    public RunEndingManager(InputManager inputManager)
    {
        _inputManager = inputManager;

        _inputManager.PlayerActions.RunEndingScreen.ReturnToHub.performed += ReturnToHub;
        _inputManager.PlayerActions.RunEndingScreen.ExitToMainMenu.performed += ReturnToMainMenu;
    }

    public void Dispose()
    {
        _inputManager.PlayerActions.RunEndingScreen.ReturnToHub.performed -= ReturnToHub;
        _inputManager.PlayerActions.RunEndingScreen.ExitToMainMenu.performed -= ReturnToMainMenu;
    }

    public void ShowRunEndingView(RunEndingStatus status)
    {
        var runEndingStatusData = runEndingStatusDatas.FirstOrDefault(statusData => statusData.Status == status);
        var runEndingView = Instantiate(runEndingViewPrefab, runEndingViewContainer.transform);
        runEndingView.SetInfo(runEndingStatusData.StatusDescription, runEndingStatusData.StatusIcon, GetActionButtonDatas());
        runEndingView.ShowAsync();
    }

    private void ReturnToHub(InputAction.CallbackContext context) => ReturnToHub();

    private void ReturnToMainMenu(InputAction.CallbackContext context) => ReturnToMainMenu();

    private void ReturnToHub() => levelLoadingManager.LoadHub();

    private void ReturnToMainMenu() => levelLoadingManager.LoadMainMenu();

    private (DetailedControlTip, UnityAction)[] GetActionButtonDatas()
    {
        var actionButtonDatas = new (string description, InputAction inputAction, UnityAction buttonPressedAction)[]
        {
            ("Вернуться в убежище", _inputManager.PlayerActions.RunEndingScreen.ReturnToHub, () => ReturnToHub()),
            ("В главное меню", _inputManager.PlayerActions.RunEndingScreen.ExitToMainMenu, () => ReturnToMainMenu())
        };
        return actionButtonDatas
            .Select(actionButtonData => (_inputManager.CreateDetailedControlsTip((actionButtonData.description, actionButtonData.inputAction)), actionButtonData.buttonPressedAction))
            .ToArray();
    }
}
