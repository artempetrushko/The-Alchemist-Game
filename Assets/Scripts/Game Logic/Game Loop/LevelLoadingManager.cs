using Controls;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoadingManager
{
    private LoadingScreenView loadingScreenViewPrefab;
    private GameObject loadingScreenViewContainer;

    private InputManager _inputManager;

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

    public LevelLoadingManager(InputManager inputManager)
    {
        _inputManager = inputManager;
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

    public void LoadMainMenu() => LoadLevelAsync(0);

    public void LoadHub() => LoadLevelAsync(1);

    public void LoadNextLocation() => LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1);

    private void ContinueSceneLoadingByInput(InputAction.CallbackContext context)
    {
        if (LoadingProgress == 1f)
        {
            isContinueButtonPressed = true;
        }
    }

    private async UniTask LoadLevelAsync(int loadingSceneIndex)
    {
        loadingScreenView = Instantiate(loadingScreenViewPrefab, loadingScreenViewContainer.transform);
        _inputManager.CurrentActionMap = PlayerInputActionMap.UI_LoadingScreen;

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
            await UniTask.Yield();
        }
    }  
}
