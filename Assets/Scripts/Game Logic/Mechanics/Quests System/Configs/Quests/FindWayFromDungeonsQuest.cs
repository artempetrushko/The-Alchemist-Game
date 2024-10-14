using UnityEngine;

namespace GameLogic.QuestSystem
{
    [CreateAssetMenu(fileName = "Find Way From Dungeons", menuName = "Game Configs/Quest System/Quests/Find Way From Dungeons")]
    public class FindWayFromDungeonsQuest : Quest
    {
        public override bool IsCompleted(GameProgress gameProgress) => gameProgress.PlayerFinishedLevelEver;
    }
}