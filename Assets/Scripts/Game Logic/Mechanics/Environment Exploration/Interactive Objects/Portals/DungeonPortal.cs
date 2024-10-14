using GameLogic.CombatSystem;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public class DungeonPortal : InteractiveObject
	{
		[SerializeField] private DungeonPortalConfig _config;
		[SerializeField] private EnemiesSpawner _enemiesSpawner;

		public DungeonPortalConfig Config => _config;
		public bool IsStable { get; set; } = false;

		public void SetInteractionAvailability(bool isInteractable) => GetComponent<Collider>().enabled = isInteractable;

		public void SetEnemySpawnerActive(bool isActive) => _enemiesSpawner.gameObject.SetActive(isActive);
	}
}