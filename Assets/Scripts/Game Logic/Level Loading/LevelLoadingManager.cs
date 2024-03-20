using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoadingManager : MonoBehaviour
{
    [SerializeField]
    private LoadingScreenView loadingScreenViewPrefab;
    [SerializeField]
    private GameObject loadingScreenViewContainer;
    [Space, SerializeField]
    private InputManager inputManager;

    private LoadingScreenView loadingScreenView;
    private float loadingProgress;
    private bool isContinueButtonPressed;

    private float LoadingProgress
    {
        get => loadingProgress;
        set
        {
            loadingProgress = value;
            loadingScreenView.SetProgressBarFillAmount(loadingProgress);
        }
    }

    public string GetCurrentLevelName()
    {
        return SceneManager.GetActiveScene().buildIndex switch
        {
            1 => "Убежище",
            2 => "Подземелья",
            _ => "Test Scene"
        };
    }

    public void LoadMainMenu() => StartCoroutine(LoadLevelAsync_COR(0));

    public void LoadHub() => StartCoroutine(LoadLevelAsync_COR(1));

    public void LoadNextLocation() => StartCoroutine(LoadLevelAsync_COR(SceneManager.GetActiveScene().buildIndex + 1));

    public void ContinueSceneLoadingByInput(InputAction.CallbackContext context)
    {
        if (context.performed && LoadingProgress == 1f)
        {
            isContinueButtonPressed = true;
        }
    }

    private IEnumerator LoadLevelAsync_COR(int loadingSceneIndex)
    {
        loadingScreenView = Instantiate(loadingScreenViewPrefab, loadingScreenViewContainer.transform);
        inputManager.CurrentActionMap = PlayerInputActionMap.UI_LoadingScreen;

        var operation = SceneManager.LoadSceneAsync(loadingSceneIndex);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                LoadingProgress = 1f;
                loadingScreenView.ShowContinueButton();
                if (isContinueButtonPressed)
                {
                    operation.allowSceneActivation = true;
                }
            }
            else
            {
                LoadingProgress = operation.progress;
            }
            yield return null;
        }
    }  
}
