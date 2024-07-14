using Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private QuestManager questManager;

    private InputManager inputManager;

    [SerializeField]
    private HudController hudManager;
    [SerializeField]
    private LevelLoadingManager levelLoadingManager;
    [SerializeField]
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
        StartCoroutine(ShowStartGameInfo_COR());
    }

    private void InitializeGameSystems()
    {
        inputManager.Initialize();
        inventoryManager.Initialize();
        questManager.Initialize(GameProgress);
    }

    private IEnumerator ShowStartGameInfo_COR()
    {
        yield return StartCoroutine(hudManager.HideStartBlackScreen_COR());
        yield return StartCoroutine(ShowLocationTitle_COR());
        questManager.UpdateQuestDescription();
    }

    private IEnumerator ShowLocationTitle_COR()
    {
        var locationName = levelLoadingManager.GetCurrentLevelName();
        yield return StartCoroutine(hudManager.ShowLocationName_COR(locationName));
    }
}
