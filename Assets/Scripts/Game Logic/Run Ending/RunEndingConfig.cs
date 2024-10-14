using Controls;
using UnityEngine;

namespace GameLogic
{
	[CreateAssetMenu(fileName = "Run Ending Config", menuName = "Game Configs/Run Ending Config")]
	public class RunEndingConfig : ScriptableObject
	{
		[SerializeField] private RunEndingScreenActionMap _actionMap;
		[SerializeField] private RunEndingStatusData[] _statusDatas;

		public RunEndingScreenActionMap ActionMap => _actionMap;
		public RunEndingStatusData[] StatusDatas => _statusDatas;
	}
}