using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.EnvironmentExploration
{
	public class InteractiveObjectConfig : ScriptableObject
	{
		[SerializeField] private LocalizedString _name;
		[SerializeField] private LocalizedString _interactionDescription;

		public LocalizedString Name => _name;
		public LocalizedString InteractionDescription => _interactionDescription;
	}
}