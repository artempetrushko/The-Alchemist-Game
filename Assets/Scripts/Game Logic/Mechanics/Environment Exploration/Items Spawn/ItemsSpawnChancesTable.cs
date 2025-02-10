using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
	[CreateAssetMenu(fileName = "New Spawn Chances Table", menuName = "Game Configs/Environment Exploration/Items Spawn Chances Table")]
	public class ItemsSpawnChancesTable : ScriptableObject
	{
		[SerializeField] private PossibleItem[] _possibleItems;

		public PossibleItem[] PossibleItems => _possibleItems;
	}
}