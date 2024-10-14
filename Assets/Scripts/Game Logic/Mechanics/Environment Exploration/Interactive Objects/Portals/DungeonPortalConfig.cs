using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
	public class DungeonPortalConfig : InteractiveObjectConfig
	{
		[Space]
		[SerializeField] private GameObject _stabilizationEffectPrefab;
		[SerializeField] private int _stabilizationTimeInSeconds;
		[Space]
		[SerializeField] private int _spawnerAppearanceDelayInSeconds;

		public GameObject StabilizationEffectPrefab => _stabilizationEffectPrefab;
		public int StabilizationTimeInSeconds => _stabilizationTimeInSeconds;
		public int SpawnerAppearanceDelayInSeconds => _spawnerAppearanceDelayInSeconds;
	}
}