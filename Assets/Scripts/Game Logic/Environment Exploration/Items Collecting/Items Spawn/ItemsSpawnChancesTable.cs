using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Chances Table", menuName = "Game Entities/Items Spawn Chances Table", order = 51)]
public class ItemsSpawnChancesTable : ScriptableObject
{
    [SerializeField]
    private List<PossibleItem> possibleItems;

    public List<ItemState> SpawnItems()
    {
        var itemStates = new List<ItemState>();
        foreach (var item in possibleItems) 
        {
            var spawnedItems = item.TrySpawn();
            if (spawnedItems.Count > 0)
            {
                itemStates = itemStates.Concat(spawnedItems).ToList();
            }
        }
        return itemStates;
    }
}
