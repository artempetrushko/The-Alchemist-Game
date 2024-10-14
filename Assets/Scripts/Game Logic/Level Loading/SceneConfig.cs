using System;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.LevelLoading
{
    [Serializable]
    public class SceneConfig
    {
        [SerializeField] private SceneType _sceneType;
        [SerializeField] private LocalizedString _name;
        [SerializeField] private int _sceneIndex;

        public SceneType SceneType => _sceneType;
        public LocalizedString Name => _name;
        public int SceneIndex => _sceneIndex;
    }
}