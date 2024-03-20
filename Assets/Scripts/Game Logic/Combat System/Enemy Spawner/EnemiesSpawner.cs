using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private ABC_StateManager enemyPrefab;
    [SerializeField]
    private int maxSpawningEnemiesCount;
    [SerializeField]
    private int spawnDelayInSeconds;

    private BoxCollider spawnArea;
    private bool enemiesSpawning = false; 

    private void OnEnable()
    {
        spawnArea = spawnPoint.GetComponent<BoxCollider>();
        StartCoroutine(SpawnEnemies_COR());
    }

    private void OnDisable()
    {
        spawnPoint.transform.DetachChildren();
        foreach (var enemy in spawnPoint.GetComponentsInChildren<ABC_StateManager>())
        {
            enemy.KillEnemy();
        }
    }

    private void Update()
    {
        if (!enemiesSpawning)
        {
            if (spawnPoint.GetComponentsInChildren<ABC_StateManager>().Length < maxSpawningEnemiesCount)
            {
                StartCoroutine(SpawnEnemies_COR());
            }
        }
    }

    private IEnumerator SpawnEnemies_COR()
    {
        enemiesSpawning = true;
        while (spawnPoint.GetComponentsInChildren<ABC_StateManager>().Length < maxSpawningEnemiesCount)
        {
            var spawnedEnemy = Instantiate(enemyPrefab, spawnPoint);
            spawnedEnemy.GetComponent<NavMeshAgent>().Warp(spawnPoint.transform.position + new Vector3(Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2),
                                                                                                       0,
                                                                                                       Random.Range(-spawnArea.size.z / 2, spawnArea.size.z / 2)));
            spawnedEnemy.GetComponent<NavMeshAgent>().enabled = true;
            yield return new WaitForSeconds(spawnDelayInSeconds);
        }
        enemiesSpawning = false;
    }
}
