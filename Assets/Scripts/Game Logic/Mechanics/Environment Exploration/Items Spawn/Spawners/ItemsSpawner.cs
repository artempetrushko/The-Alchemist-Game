using System;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class ItemsSpawner : MonoBehaviour
    {
        [SerializeField]
        protected ItemsSpawnChancesTable spawnChancesTable;

        protected void SpawnItems(Func<Vector3> setItemPositionFunc)
        {
            var spawnedItemStates = spawnChancesTable.SpawnItems();
            foreach (var itemState in spawnedItemStates)
            {
                var spawnedItem = Instantiate(itemState.BaseParams.PhysicalRepresentation);
                spawnedItem.CurrentItemState = itemState;
                spawnedItem.transform.position = setItemPositionFunc.Invoke();
            }
        }
    }
}