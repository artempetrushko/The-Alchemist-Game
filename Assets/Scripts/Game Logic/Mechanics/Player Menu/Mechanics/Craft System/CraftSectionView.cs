using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class CraftSectionView : PlayerMenuSectionView
    {
        [Space, SerializeField]
        private RecipesSectionView recipesSectionView;
        [SerializeField]
        private ItemCraftingSectionView itemCreationSectionView;
        [SerializeField]
        private CraftingProcessStateView craftingProcessStateView;
        [SerializeField]
        private CraftInventorySubsectionView inventorySubsectionView;

        public RecipesSectionView RecipesSectionView => recipesSectionView;
        public ItemCraftingSectionView ItemCreationSectionView => itemCreationSectionView;
        public CraftingProcessStateView CraftingProcessStateView => craftingProcessStateView;
        public CraftInventorySubsectionView InventorySubsectionView => inventorySubsectionView;
    }
}