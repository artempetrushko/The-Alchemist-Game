using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(fileName = "Item Interactions Config", menuName = "Game Data/Item Interactions/Item Interactions Config")]
    public class ItemInteractionsConfig : ScriptableObject
    {
        [SerializeField] private ItemInteractionData[] _interactionDatas;

        public ItemInteractionData[] InteractionDatas => _interactionDatas;
    }
}
