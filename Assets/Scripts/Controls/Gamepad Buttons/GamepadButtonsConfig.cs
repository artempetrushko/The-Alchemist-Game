using UnityEngine;

namespace Controls
{
    [CreateAssetMenu(fileName = "Gamepad Buttons Config", menuName = "Game Configs/Input/Gamepad Buttons Config")]
    public class GamepadButtonsConfig : ScriptableObject
    {
        [SerializeField] private GamepadButtonConfig[] _gamepadButtonConfigs;

        public GamepadButtonConfig[] GamepadButtonConfigs => _gamepadButtonConfigs;
    }
}