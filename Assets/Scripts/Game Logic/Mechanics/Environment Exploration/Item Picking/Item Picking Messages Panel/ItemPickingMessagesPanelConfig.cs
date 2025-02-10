using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    [CreateAssetMenu(fileName = "Item Picking Messages Panel Config", menuName = "Game Configs/Environment Exploration/Item Picking Messages Panel Config")]
	public class ItemPickingMessagesPanelConfig : ScriptableObject
	{
        [SerializeField] private int _maxMessagesCount;
        [SerializeField] private int _messagesShowingTimeInSeconds;

		public int MaxMessagesCount => _maxMessagesCount;
		public int MessagesShowingTimeInSeconds => _messagesShowingTimeInSeconds;
    }
}