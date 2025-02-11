using GameLogic.LevelLoading;

namespace EventBus
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