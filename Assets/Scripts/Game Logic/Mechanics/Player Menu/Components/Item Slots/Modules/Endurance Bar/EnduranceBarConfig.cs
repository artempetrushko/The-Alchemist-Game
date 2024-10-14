using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [CreateAssetMenu(fileName = "Endurance Bar Config", menuName = "Game Configs/Player Menu/Endurance Bar Config")]
    public class EnduranceBarConfig : ScriptableObject
    {
        [SerializeField] private EnduranceBarState[] _states;

        public EnduranceBarState[] States => _states;
    }
}