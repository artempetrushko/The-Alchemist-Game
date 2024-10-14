namespace GameLogic.QuestSystem
{
    public interface IProgressiveQuest
    {
        string GetFormattedProgress(GameProgress gameProgress);
    }
}