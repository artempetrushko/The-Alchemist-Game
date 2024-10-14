using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(fileName = "Health Counter Config", menuName = "Game Configs/Player/Health Counter Config")]
    public class HealthCounterConfig : ScriptableObject
    {
        [SerializeField] private HealthBarState[] _healthBarStates;

        public HealthBarState[] HealthBarStates => _healthBarStates;
    }
}