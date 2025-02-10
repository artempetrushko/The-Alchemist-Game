using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLootSpawner : ItemsSpawner
{
    private BoxCollider spawnArea;

    private void OnEnable()
    {
        spawnArea = GetComponent<BoxCollider>();
        SpawnItems(() => new Vector3(transform.position.x + Random.Range(spawnArea.size.x / 2, -spawnArea.size.x / 2),
                                     transform.position.y + spawnArea.center.y,
                                     transform.position.z + Random.Range(spawnArea.size.z / 2, -spawnArea.size.z / 2)));
    }
}
