using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PossibleItem
{
    [SerializeField] private ItemData item;
    [Range(0, 100)]
    [SerializeField] private float spawnChance;   
    [SerializeField] private int minCount;
    [SerializeField] private int maxCount;
    [SerializeField] private AnimationCurve itemsCountDistributionGraph;

    private int ItemsCount
    {
        get
        {
            if (itemsCountDistributionGraph.length == 0)
            {
                return Random.Range(minCount, maxCount);
            }
            var chance = Random.Range(0f, 100f);
            var maxCountPercentage = itemsCountDistributionGraph.Evaluate(chance) / 100;
            return Random.Range((int)MathF.Floor(minCount + (maxCount - minCount) * maxCountPercentage), maxCount);
        }
    }

    public List<ItemState> TrySpawn()
    {
        var spawnedItems = new List<ItemState>();
        switch (item)
        {
            case StackableItemData:
                if (IsItemWillSpawned())
                {
                    var itemState = item.GetItemState() as StackableItemState;
                    itemState.ItemsCount = ItemsCount;
                    spawnedItems.Add(itemState);
                }
                break;

            default:
                if (IsItemWillSpawned())
                {
                    var generatedItemsCount = ItemsCount;
                    for (var i = 0; i < generatedItemsCount; i++)
                    {
                        spawnedItems.Add(item.GetItemState());
                    }
                }
                break;
        }
        return spawnedItems;
    }

    private bool IsItemWillSpawned() => Random.Range(1f, 100f) > 100 - spawnChance;
}
