using UnityEngine;

namespace GameLogic.CombatSystem
{
    [CreateAssetMenu(fileName = "Spawn Config", menuName = "Game Configs/Combat System/Spawn Config")]
    public class SpawnConfig : ScriptableObject
    {
        [SerializeField] private ABC_StateManager _enemyPrefab;
        [SerializeField] private int _maxSpawningEnemiesCount;
        [SerializeField] private int _spawnDelayInSeconds;

        public ABC_StateManager EnemyPrefab => _enemyPrefab;
        public int MaxSpawningEnemiesCount => _maxSpawningEnemiesCount;
        public int SpawnDelayInSeconds => _spawnDelayInSeconds;
    }
}
