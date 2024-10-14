using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
	public class ItemsSpawnerGroupsContainer : MonoBehaviour
	{
		[SerializeField] private ItemsSpawnerGroupData[] _itemsSpawnerGroupDatas;

		public ItemsSpawnerGroupData[] ItemsSpawnerGroupDatas => _itemsSpawnerGroupDatas;
	}
}