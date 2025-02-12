using GameLogic.QuestSystem;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Game Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private LocalizedString _locationName;
        [SerializeField] private Quest _startQuest;

        public LocalizedString LocationName => _locationName;
        public Quest StartQuest => _startQuest;
    }
}