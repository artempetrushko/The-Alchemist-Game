using System;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
	[Serializable]
	public class ItemsSpawnerGroupData
	{
		[SerializeField] private ItemsSpawnChancesTable _spawnChancesTable;
		[SerializeField] private SphereCollider[] _spawnAreas;
		
		public ItemsSpawnChancesTable SpawnChancesTable => _spawnChancesTable;
		public SphereCollider[] SpawnAreas => _spawnAreas;
	}
}