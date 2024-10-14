using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
	public abstract class InteractiveObject : MonoBehaviour
	{
		[SerializeField] protected InteractiveObjectConfig _baseParams;

		public InteractiveObjectConfig BaseParams => _baseParams;
	}
}