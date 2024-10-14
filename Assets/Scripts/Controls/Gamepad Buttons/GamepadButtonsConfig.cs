using UnityEngine;

namespace Controls
{
    [CreateAssetMenu(fileName = "Gamepad Buttons Config", menuName = "Game Configs/Controls/Gamepad Buttons Config")]
	public class GamepadButtonsConfig : ScriptableObject
	{
		[SerializeField] private GamepadButtonConfig[] _gamepadButtonConfigs;

		public GamepadButtonConfig[] GamepadButtonConfigs => _gamepadButtonConfigs;
	}
}