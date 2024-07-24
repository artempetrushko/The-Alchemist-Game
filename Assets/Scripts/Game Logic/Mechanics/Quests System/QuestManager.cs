using Controls;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.Hud;
using UnityEngine.InputSystem;

public class QuestManager : IDisposable
{
    private QuestProgressView questProgressView;
    private int requiredStabilizerPartsCount;
    private ItemData stabilizerPartDataTemplate;
    private ItemData stabilizerDataTemplate;

    private GameProgress gameProgress;
    private InputManager _inputManager;

    public int StabilizerPartsCount
    {
        get => gameProgress.StabilizerPartsCount;
        set
        {
            if (value != gameProgress.StabilizerPartsCount)
            {
                gameProgress.StabilizerPartsCount = value;
                ShowQuestProgressAsync();
            }
        }
    }
    public bool StabilizerCreatedAndAvailable
    {
        get => gameProgress.StabilizerCreatedAndAvailable;
        set
        {
            if (value != gameProgress.StabilizerCreatedAndAvailable)
            {
                gameProgress.StabilizerCreatedAndAvailable = value;
                ShowQuestProgressAsync();
            }
        }
    }

    public QuestManager()
    {
        _inputManager.PlayerActions.Player.ShowQuestProgress.performed += ShowQuestProgress;
    }

    public void Dispose()
    {
        _inputManager.PlayerActions.Player.ShowQuestProgress.performed -= ShowQuestProgress;
    }

    public void Initialize(GameProgress gameProgress)
    {
        this.gameProgress = gameProgress;
    }

    private void ShowQuestProgress(InputAction.CallbackContext context) => ShowQuestProgressAsync().Forget();

    private async UniTask ShowQuestProgressAsync() => await UniTask.Yield();//(questProgressView.ShowQuestDescription_COR());

    public void UpdateQuestDescription() => questProgressView.SetQuestDescriptionText(GetMainQuestDescription());

    public void UpdateQuestItemsInfo(ItemState[] inventoryItems)
    {
        var stabilizerParts = inventoryItems.Where(item => item is ResourceState && item.BaseParams.Title == "Часть стабилизатора").ToList();
        StabilizerPartsCount = stabilizerParts.Count > 0
            ? stabilizerParts.Sum(item => (item as ResourceState).ItemsCount)
            : 0;
        StabilizerCreatedAndAvailable = inventoryItems.Any(item => item is ResourceState && item.BaseParams.Title == "Стабилизатор портала");
    }

    public List<ItemState> CreateSavedQuestItems()
    {
        var specialItems = new List<ItemState>();
        if (gameProgress.StabilizerPartsCount > 0)
        {
            var stabilizerParts = stabilizerPartDataTemplate.GetItemState();
            (stabilizerParts as ResourceState).ItemsCount = StabilizerPartsCount;
            specialItems.Add(stabilizerParts);
        }
        if (gameProgress.StabilizerCreatedAndAvailable)
        {
            specialItems.Add(stabilizerDataTemplate.GetItemState());
        }
        return specialItems;
    }

    private string GetMainQuestDescription()
    {
        if (!gameProgress.PlayerFinishedLevelEver)
        {
            return "Найдите способ выбраться из подземелий";
        }
        if (gameProgress.StabilizerCreatedAndAvailable)
        {
            return "Стабилизируйте портал и сбегите";
        }
        if (gameProgress.StabilizerPartsCount >= requiredStabilizerPartsCount)
        {
            return "Изготовьте стабилизатор";
        }
        return $"Найдите части стабилизатора для портала ({gameProgress.StabilizerPartsCount}/{requiredStabilizerPartsCount})";
    }
}
