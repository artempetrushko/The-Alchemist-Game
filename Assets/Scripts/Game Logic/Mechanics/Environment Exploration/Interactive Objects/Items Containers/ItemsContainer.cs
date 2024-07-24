using System.Collections.Generic;
using UnityEngine;

public class ItemsContainer : InteractiveObject
{
    [SerializeField] private ItemsSpawnChancesTable spawnChancesTable;
    [SerializeField] private GameObject filledContainerEffect;

    private List<ItemState> spawnedItems = new();

    public List<ItemState> GetContainingItems()
    {
        if (spawnedItems.Count == 0)
        {
            spawnedItems = spawnChancesTable.SpawnItems();
        }
        return spawnedItems;
    }

    public void SetFilledContainerEffectActive(bool isActive)
    {
        if (filledContainerEffect != null)
        {
            filledContainerEffect.SetActive(isActive);
        }      
    }

    private void OnEnable()
    {
        SetFilledContainerEffectActive(true);
    }
}
