namespace GameLogic.LevelLoading
{
    public class NextSceneSelectedSignal
    {
        public readonly SceneType SceneType;

        public NextSceneSelectedSignal(SceneType sceneType)
        {
            SceneType = sceneType;
        }
    }
}