using GameLogic.QuestSystem;
using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Game Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private Quest _startQuest;

        public Quest StartQuest => _startQuest;
    }
}