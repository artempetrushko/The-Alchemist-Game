using UnityEngine;

namespace GameLogic.QuestSystem
{
    [CreateAssetMenu(fileName = "Stabilize Portal", menuName = "Game Configs/Quest System/Quests/Stabilize Portal")]
    public class StabilizePortalQuest : Quest
    {
        public override bool IsCompleted(GameProgress gameProgress) => gameProgress.PlayerFinishedLevelThrowStabilizedPortal;
    }
}