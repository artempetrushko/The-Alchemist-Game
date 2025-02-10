using System.Collections.Generic;
using System.Linq;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    public static class ItemsSpawnManager
    {
        public static List<Item> SpawnItems(ItemsSpawnChancesTable itemsSpawnChancesTable)
        {
            var itemStates = new List<Item>();
            foreach (var item in itemsSpawnChancesTable.PossibleItems)
            {
                var spawnedItems = TrySpawnItem(item);
                if (spawnedItems.Count > 0)
                {
                    itemStates = itemStates.Concat(spawnedItems).ToList();
                }
            }
            return itemStates;
        }

        private static List<Item> TrySpawnItem(PossibleItem possibleItem)
        {
            var spawnedItems = new List<Item>();
            if (Random.Range(1f, 100f) > 100 - possibleItem.SpawnChance)
            {
                switch (possibleItem.Item)
                {
                    case StackableItemConfig:
                        var itemState = possibleItem.Item.CreateItem() as StackableItem;
                        itemState.Count.Value = GetSpawnedItemsCount(possibleItem);
                        spawnedItems.Add(itemState);
                        break;

                    default:
                        var generatedItemsCount = GetSpawnedItemsCount(possibleItem);
                        for (var i = 0; i < generatedItemsCount; i++)
                        {
                            spawnedItems.Add(possibleItem.Item.CreateItem());
                        }
                        break;
                }
            }	
            return spawnedItems;
        }

        private static int GetSpawnedItemsCount(PossibleItem possibleItem) => Random.Range(possibleItem.MinCount, possibleItem.MaxCount);
    }
}