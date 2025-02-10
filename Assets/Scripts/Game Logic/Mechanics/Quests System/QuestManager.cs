using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private QuestProgressView questProgressView;
    [Space, SerializeField]
    private int requiredStabilizerPartsCount;
    [SerializeField]
    private ItemData stabilizerPartDataTemplate;
    [SerializeField]
    private ItemData stabilizerDataTemplate;

    private GameProgress gameProgress;

    public int StabilizerPartsCount
    {
        get => gameProgress.StabilizerPartsCount;
        set
        {
            if (value != gameProgress.StabilizerPartsCount)
            {
                gameProgress.StabilizerPartsCount = value;
                ShowQuestProgress();
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
                ShowQuestProgress();
            }
        }
    }

    public void Initialize(GameProgress gameProgress)
    {
        this.gameProgress = gameProgress;
    }

    public void ShowQuestProgress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShowQuestProgress();
        }
    }

    public void ShowQuestProgress() => StartCoroutine(questProgressView.ShowQuestDescription_COR());

    public void UpdateQuestDescription() => questProgressView.SetQuestDescription(GetMainQuestDescription());

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
