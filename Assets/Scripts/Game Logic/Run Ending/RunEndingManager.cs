using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum RunEndingStatus
{
    Death,
    Completion
}

public class RunEndingManager : MonoBehaviour
{
    [SerializeField]
    private RunEndingStatusData[] runEndingStatusDatas;
    [Space, SerializeField]
    private RunEndingView runEndingView;
    [Space, SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private LevelLoadingManager levelLoadingManager;

    public void ShowRunEndingView(RunEndingStatus status)
    {
        var runEndingStatusData = runEndingStatusDatas.FirstOrDefault(statusData => statusData.Status == status);

        runEndingView.gameObject.SetActive(true);
        runEndingView.SetInfo(runEndingStatusData.StatusDescription, runEndingStatusData.StatusIcon, GetActionButtonDatas());
        StartCoroutine(runEndingView.Show_COR());
    }

    public void ReturnToHub(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReturnToHub();
        }
    }

    public void ReturnToMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReturnToMainMenu();
        }
    }

    private void ReturnToHub() => levelLoadingManager.LoadHub();

    private void ReturnToMainMenu() => levelLoadingManager.LoadMainMenu();

    private (DetailedControlTip, UnityAction)[] GetActionButtonDatas()
    {
        var actionButtonDatas = new (string description, InputAction inputAction, UnityAction buttonPressedAction)[]
        {
            ("��������� � �������", inputManager.PlayerActions.RunEndingScreen.ReturnToHub, () => ReturnToHub()),
            ("� ������� ����", inputManager.PlayerActions.RunEndingScreen.ExitToMainMenu, () => ReturnToMainMenu())
        };
        return actionButtonDatas
            .Select(actionButtonData => (inputManager.CreateDetailedControlsTip((actionButtonData.description, actionButtonData.inputAction)), actionButtonData.buttonPressedAction))
            .ToArray();
    }
}
