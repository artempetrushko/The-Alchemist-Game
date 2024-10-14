using System.Collections.Generic;
using System.Linq;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
	public class ItemsSpawnManager
	{
        public static List<ItemState> SpawnItems(ItemsSpawnChancesTable itemsSpawnChancesTable)
        {
            var itemStates = new List<ItemState>();
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

        public void SpawnEnvironmentItems(ItemsSpawnChancesTable itemsSpawnChancesTable, SphereCollider spawnArea)
		{
			var spawnedItemStates = SpawnItems(itemsSpawnChancesTable);
			foreach (var itemState in spawnedItemStates)
			{
				var spawnedItem = Object.Instantiate(itemState.PhysicalRepresentation);
				spawnedItem.transform.position += GetSpawnPointOffset(spawnArea);
			}
		}

		private static List<ItemState> TrySpawnItem(PossibleItem possibleItem)
		{
			var spawnedItems = new List<ItemState>();
			if (Random.Range(1f, 100f) > 100 - possibleItem.SpawnChance)
			{
				switch (possibleItem.Item)
				{
					case StackableItemConfig:
						var itemState = possibleItem.Item.CreateItem() as StackableItemState;
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

		private Vector3 GetSpawnPointOffset(SphereCollider spawnArea)
		{
			var radius = Random.Range(-spawnArea.radius, spawnArea.radius);
			var theta = Random.Range(-180, 180) * Mathf.PI / 180;

			return new Vector3(radius * Mathf.Cos(theta), 0, radius * Mathf.Sin(theta));
		}
	}
}