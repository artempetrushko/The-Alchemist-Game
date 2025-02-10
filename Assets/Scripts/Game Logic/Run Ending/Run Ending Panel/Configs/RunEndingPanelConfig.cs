using Controls;
using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(fileName = "Run Ending Config", menuName = "Game Configs/Run Ending Config")]
    public class RunEndingPanelConfig : ScriptableObject
    {
        [SerializeField] private RunEndingPanelActionMap _actionMap;
        [SerializeField] private RunEndingStatusData[] _statusDatas;

        public RunEndingPanelActionMap ActionMap => _actionMap;
        public RunEndingStatusData[] StatusDatas => _statusDatas;
    }
}