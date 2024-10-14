using System.Linq;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.QuestSystem
{
    [CreateAssetMenu(fileName = "Create Stabilizer", menuName = "Game Configs/Quest System/Quests/Create Stabilizer")]
    public class CreateStabilizerQuest : Quest
    {
        [SerializeField] private ItemConfig _stabilizerTemplate;

        public override bool IsCompleted(GameProgress gameProgress) => gameProgress.QuestItems.Any(item => item.Id == _stabilizerTemplate.Id);
    }
}