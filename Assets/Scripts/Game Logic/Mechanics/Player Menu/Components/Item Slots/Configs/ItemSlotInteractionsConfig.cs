using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [CreateAssetMenu(fileName = "Item Slot Interactions Config", menuName = "Game Configs/Player Menu/Item Slot Interactions Config")]
    public class ItemSlotInteractionsConfig : ScriptableObject
    {
        [SerializeField] private ItemsInteraction[] _interactions;

        public ItemsInteraction[] Interactions => _interactions;
    }
}