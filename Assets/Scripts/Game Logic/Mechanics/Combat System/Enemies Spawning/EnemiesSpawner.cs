using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace GameLogic.CombatSystem
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnConfig _spawnConfig;
        [SerializeField] private Collider _spawnArea;

        private bool _areEnemiesSpawning = false;

        private void OnEnable()
        {
            SpawnEnemiesAsync().Forget();
        }

        private void OnDisable()
        {
            _spawnArea.transform.DetachChildren();
            foreach (var enemy in _spawnArea.GetComponentsInChildren<ABC_StateManager>())
            {
                enemy.KillEnemy();
            }
        }

        private void Update()
        {
            if (!_areEnemiesSpawning)
            {
                if (_spawnArea.GetComponentsInChildren<ABC_StateManager>().Length < _spawnConfig.MaxSpawningEnemiesCount)
                {
                    SpawnEnemiesAsync().Forget();
                }
            }
        }

        private async UniTask SpawnEnemiesAsync()
        {
            _areEnemiesSpawning = true;
            while (_spawnArea.GetComponentsInChildren<ABC_StateManager>().Length < _spawnConfig.MaxSpawningEnemiesCount)
            {
                var spawnedEnemy = Instantiate(_spawnConfig.EnemyPrefab, _spawnArea.transform);
                spawnedEnemy.GetComponent<NavMeshAgent>().Warp(_spawnArea.transform.position + new Vector3(Random.Range(-_spawnArea.bounds.size.x / 2, _spawnArea.bounds.size.x / 2),
                                                                                                           0,
                                                                                                           Random.Range(-_spawnArea.bounds.size.z / 2, _spawnArea.bounds.size.z / 2)));
                spawnedEnemy.GetComponent<NavMeshAgent>().enabled = true;
                await UniTask.WaitForSeconds(_spawnConfig.SpawnDelayInSeconds);
            }
            _areEnemiesSpawning = false;
        }
    }
}