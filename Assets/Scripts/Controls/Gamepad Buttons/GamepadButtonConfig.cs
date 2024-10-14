using System;
using UnityEngine;

namespace Controls
{
    [Serializable]
	public class GamepadButtonConfig
	{
		[SerializeField] private Sprite _icon;
		[SerializeField] private string[] _displayNames;

		public Sprite Icon => _icon;
		public string[] DisplayNames => _displayNames;
	}
}