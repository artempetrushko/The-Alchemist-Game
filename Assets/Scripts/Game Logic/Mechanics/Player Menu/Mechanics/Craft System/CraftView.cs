using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
    public class CraftView : MonoBehaviour
    {
        [SerializeField] private ItemCraftingSectionView _itemCraftingSectionView;
        [SerializeField] private ItemCraftingStatusPanelView _itemCraftingStatusPanelView;

        public ItemCraftingSectionView ItemCraftingSectionView => _itemCraftingSectionView;
        public ItemCraftingStatusPanelView ItemCraftingStatusPanelView => _itemCraftingStatusPanelView;
    }
}