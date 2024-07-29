using Controls;
using Cysharp.Threading.Tasks;
using GameLogic;
using GameLogic.Inventory;
using UI.Hud;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private QuestManager questManager;

    private InputManager inputManager;

    private HUDController hudManager;
    private LevelLoadingManager levelLoadingManager;
    private RunEndingManager runEndingManager;

    public GameProgress GameProgress { get; private set; }

    public void GoToNextLocation() => levelLoadingManager.LoadNextLocation();

    public void FinishRun()
    {
        if (!GameProgress.PlayerFinishedLevelEver)
        {
            GameProgress.PlayerFinishedLevelEver = true;
        }
        runEndingManager.ShowRunEndingView(RunEndingStatus.Completion);
        //SavePlayerProgress();
        Destroy(GetComponent<Collider>());
    }

    private void Start()
    {
        InitializeGameSystems();
        ShowStartGameInfoAsync().Forget();
    }

    private void InitializeGameSystems()
    {
        inputManager.Initialize();
        inventoryManager.Initialize();
        questManager.Initialize(GameProgress);
    }

    private async UniTask ShowStartGameInfoAsync()
    {
        await hudManager.HideStartBlackScreenAsync();

        var locationName = levelLoadingManager.GetCurrentLevelName();
        await hudManager.ShowLocationNameAsync(locationName);

        questManager.UpdateQuestDescription();
    }
}
