using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    [CreateAssetMenu(fileName = "Item Picking Messages Section Config", menuName = "Game Configs/Environment Exploration/Item Picking Messages Section Config")]
	public class ItemPickingMessagesSectionConfig : ScriptableObject
	{
        [SerializeField] private int _maxMessagesCount;
        [SerializeField] private int _messagesShowingTimeInSeconds;

		public int MaxMessagesCount => _maxMessagesCount;
		public int MessagesShowingTimeInSeconds => _messagesShowingTimeInSeconds;
    }
}