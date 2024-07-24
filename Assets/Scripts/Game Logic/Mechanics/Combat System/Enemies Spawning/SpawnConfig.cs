using UnityEngine;

namespace GameLogic.CombatSystem
{
    [CreateAssetMenu(fileName = "Spawn Config", menuName = "Game Data/Combat System/Spawn Config")]
    public class SpawnConfig : ScriptableObject
    {
        [SerializeField] private ABC_StateManager[] _enemyPrefabs;
        [SerializeField] private int _maxSpawningEnemiesCount;
        [SerializeField] private int _spawnDelayInSeconds;

        public ABC_StateManager[] EnemyPrefabs => _enemyPrefabs;
        public int MaxSpawningEnemiesCount => _maxSpawningEnemiesCount;
        public int SpawnDelayInSeconds => _spawnDelayInSeconds;
    }
}
