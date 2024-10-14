using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.QuestSystem
{
    public abstract class Quest : ScriptableObject
    {
        [SerializeField] private LocalizedString _description;
        [SerializeField] private Quest _nextQuest;

        public LocalizedString Description => _description;
        public Quest NextQuest => _nextQuest;

        public abstract bool IsCompleted(GameProgress gameProgress);
    }
}