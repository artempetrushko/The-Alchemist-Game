using System.Collections;
using Controls;
using Cysharp.Threading.Tasks;
using GameLogic.HUD;
using GameLogic.LevelLoading;
using GameLogic.PlayerMenu;
using GameLogic.QuestSystem;
using GameLogic.RunEnding;
using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private QuestManager _questManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private HUDManager _hudManager;
        [SerializeField] private LevelLoadingManager _levelLoadingManager;
        [SerializeField] private RunEndingManager _runEndingManager;

        public GameProgress GameProgress { get; private set; }

        [Inject]
        public void Construct(HUDManager hudManager)
        {
            _hudManager = hudManager;
        }

        public void GoToNextLocation() => _levelLoadingManager.LoadNextLocation();

        public void FinishRun()
        {
            if (!GameProgress.PlayerFinishedLevelEver)
            {
                GameProgress.PlayerFinishedLevelEver = true;
            }
            _runEndingManager.ShowRunEndingView(RunEndingStatus.Completion);
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
            _inputManager.Initialize();
            _inventoryManager.Initialize();
            _questManager.Initialize(GameProgress);
        }

        private IEnumerator ShowStartGameInfo_COR()
        {
            yield return StartCoroutine(_hudManager.HideStartBlackScreenAsync().ToCoroutine());
            yield return StartCoroutine(ShowLocationTitle_COR());
            _questManager.UpdateQuestDescription();
        }

        private IEnumerator ShowLocationTitle_COR()
        {
            var locationName = _levelLoadingManager.GetCurrentLevelName();
            yield return StartCoroutine(_hudManager.ShowLocationNameAsync(locationName).ToCoroutine());
        }
    }
}