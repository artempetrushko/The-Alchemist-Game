using Controls;
using UnityEngine;

namespace GameLogic.LevelLoading
{
    [CreateAssetMenu(fileName = "Level Loading Config", menuName = "Game Configs/Levels/Level Loading Config")]
    public class LevelLoadingConfig : ScriptableObject
    {
        [SerializeField] private SceneConfig[] _sceneConfigs;
        [SerializeField] private LoadingScreenActionMap _actionMap;

        public SceneConfig[] SceneConfigs => _sceneConfigs;
        public LoadingScreenActionMap ActionMap => _actionMap;
    }
}