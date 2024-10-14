using UnityEngine;

namespace Controls
{
    [CreateAssetMenu(fileName = "Input Config", menuName = "Game Configs/Input/Input Config")]
    public class InputConfig : ScriptableObject
    {
        [SerializeField] private string _gamepadSchemeName;
        [SerializeField] private string _keyboardMouseSchemeName;
        [SerializeField] private PlayerInputActionMap _defaultActionMap;
        [SerializeField] private GamepadButtonConfig[] _gamepadButtonConfigs;

        public string GamepadSchemeName => _gamepadSchemeName;
        public string KeyboardMouseSchemeName => _keyboardMouseSchemeName;
        public PlayerInputActionMap DefaultActionMap => _defaultActionMap;
        public GamepadButtonConfig[] GamepadButtonConfigs => _gamepadButtonConfigs;
    }
}