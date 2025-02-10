using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private ItemsSpawnChancesTable _spawnChancesTable;
        [SerializeField] private SphereCollider _spawnArea;

        private void OnEnable()
        {
            SpawnItems();
        }

        private void SpawnItems()
        {
            var spawnedItems = ItemsSpawnManager.SpawnItems(_spawnChancesTable);
            foreach (var spawnedItem in spawnedItems)
            {
                var pickableItem = Instantiate(spawnedItem.PhysicalRepresentation, _spawnArea.transform);
                pickableItem.transform.position += GetSpawnPointOffset();
                pickableItem.ItemState = spawnedItem;
            }
        }

        private Vector3 GetSpawnPointOffset()
        {
            var radius = Random.Range(-_spawnArea.radius, _spawnArea.radius);
            var theta = Random.Range(-180, 180) * Mathf.PI / 180;

            return new Vector3(radius * Mathf.Cos(theta), 0, radius * Mathf.Sin(theta));
        }
    }
}