using UnityEngine;

namespace GameLogic.LootSystem
{
    public class EnemiesDropSpawner : ItemsSpawner
    {
        [SerializeField]
        private Transform itemsSpawnPosition;
        [SerializeField]
        private int spawnAreaRadius;

        protected void OnDisable()
        {
            SpawnItems(() => new Vector3(itemsSpawnPosition.position.x + Random.Range(-spawnAreaRadius, spawnAreaRadius),
                                         itemsSpawnPosition.position.y,
                                         itemsSpawnPosition.position.z + Random.Range(-spawnAreaRadius, spawnAreaRadius)));
        }
    }
}