using System.Linq;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.QuestSystem
{
    [CreateAssetMenu(fileName = "Find Stabilizer Parts", menuName = "Game Configs/Quest System/Quests/Find Stabilizer Parts")]
    public class FindStabilizerPartsQuest : Quest, IProgressiveQuest
    {
        [SerializeField] private ItemConfig _stabilizerPartTemplate;
        [SerializeField] private int _requiredStabilizerPartsCount;

        public string GetFormattedProgress(GameProgress gameProgress) => $"{GetCollectedStabilizerPartsCount(gameProgress)}/{_requiredStabilizerPartsCount}";

        public override bool IsCompleted(GameProgress gameProgress) => GetCollectedStabilizerPartsCount(gameProgress) >= _requiredStabilizerPartsCount;

        private int GetCollectedStabilizerPartsCount(GameProgress gameProgress) => gameProgress.QuestItems.Count(item => item.Id == _stabilizerPartTemplate.Id);
    }
}